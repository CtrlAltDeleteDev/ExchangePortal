using Microsoft.Extensions.Hosting;

namespace Exchange.Portal.ApplicationCore.Jobs;

public class RefreshExchangeRateBackgroundJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private const int DelayInMilliseconds = 10000;
    public RefreshExchangeRateBackgroundJob(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
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
            //use logger
            Console.WriteLine(e);
            throw;
        }
    }
}