namespace Exchange.Portal.ApplicationCore;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHttpClient();
        serviceCollection.AddHttpClient("TelegramBotClient")
            .AddTypedClient<ITelegramBotClient>((httpClient, sp) =>
            {
                TelegramBotSettings botConfig = sp.GetRequiredService<TelegramBotSettings>();
                TelegramBotClientOptions options = new(botConfig.Token);
                return new TelegramBotClient(options, httpClient);
            });
        
        
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
}