using System.Net;
using System.Text.Json;
using Exchange.Portal.ApplicationCore.HttpClients;
using Polly;
using Polly.Extensions.Http;
using Refit;
namespace Exchange.Portal.ApplicationCore;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection, BinanceClientSettings binanceClientSettings)
    {
        ConfigureInternalHttpClients(serviceCollection, binanceClientSettings);

        serviceCollection.AddMediatR(Assembly.GetExecutingAssembly());

        serviceCollection.AddSingleton<ITimeProviderService, TimeProviderService>();
        
        serviceCollection.AddScoped<IInitiateRateExchange, InitiateRateExchange>();
        serviceCollection.AddScoped<IRefreshRateService, RefreshRateService>();
        serviceCollection.AddScoped<IExchangeRateAccumulation, WebClientExchangeRateAccumulation>();
        serviceCollection.AddScoped<IExchangeRateAccumulation, TransferExchangeRateAccumulation>();
        serviceCollection.AddScoped<IMigrationService, MigrationService>();

        serviceCollection.AddHostedService<RefreshExchangeRateBackgroundJob>();

        return serviceCollection;
    }

    private static void ConfigureInternalHttpClients(IServiceCollection serviceCollection,
        BinanceClientSettings binanceClientSettings)
    {
        serviceCollection.AddHttpClient();
        
        serviceCollection.AddHttpClient("TelegramBotClient")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                TelegramBotSettings botConfig = sp.GetRequiredService<TelegramBotSettings>();
                TelegramBotClientOptions options = new(botConfig.Token);
                return new TelegramBotClient(options, httpClient);
            })
            .AddPolicyHandler(
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt))));;

        serviceCollection.AddRefitClient<IBinanceClient>(settings => new RefitSettings
            {
                ContentSerializer =
                    new SystemTextJsonContentSerializer(new JsonSerializerOptions(JsonSerializerDefaults.Web)),
            })
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(binanceClientSettings.Url))
            .AddPolicyHandler((services, request) =>
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
                    .OrResult(msg => msg.StatusCode == HttpStatusCode.TooManyRequests)
                    .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(3, retryAttempt)),
                        onRetry: (_, timespan, retryAttempt, _) =>
                        {
                            services.GetRequiredService<ILogger<IBinanceClient>>()?
                                .LogWarning("Delaying for {delay}ms, then making retry {retry}.", timespan.TotalMilliseconds, retryAttempt);
                        }));
    }
}