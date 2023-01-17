using Microsoft.Extensions.Hosting;

namespace Exchange.Portal.ApplicationCore.Jobs;

internal class RefreshExchangeRateBackgroundJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RefreshExchangeRateBackgroundJob> _logger;

    private const int DelayInMilliseconds = 10000;

    public RefreshExchangeRateBackgroundJob(IServiceProvider serviceProvider,
        ILogger<RefreshExchangeRateBackgroundJob> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();
            var refreshRateService = scope.ServiceProvider.GetRequiredService<IRefreshRateService>();
        
            while (!stoppingToken.IsCancellationRequested)
            {
                await refreshRateService.SyncRatesAsync(stoppingToken);

                await Task.Delay(DelayInMilliseconds, stoppingToken);
            }
        }
        catch (Exception e)
        {
            _logger.LogError(e, "The refresh exchange rate background job has failed");
        }
    }
}