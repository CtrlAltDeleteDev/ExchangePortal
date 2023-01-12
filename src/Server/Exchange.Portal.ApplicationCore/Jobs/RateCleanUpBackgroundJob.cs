using Microsoft.Extensions.Hosting;

namespace Exchange.Portal.ApplicationCore.Jobs;

//This job is needed to remove all old records in db since there no need to accumulate many rates
//Additionally, hosting has restriction on the amount of record allowed to store (10K) 
internal class RateCleanUpBackgroundJob : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ITimeProviderService _timeProvider;
    
    private const int DelayInHours = 2;

    public RateCleanUpBackgroundJob(IServiceProvider serviceProvider, ITimeProviderService timeProvider)
    {
        _serviceProvider = serviceProvider;
        _timeProvider = timeProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await using AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();
        ICleanupRateService cleanupRateService = scope.ServiceProvider.GetRequiredService<ICleanupRateService>();

        while (!stoppingToken.IsCancellationRequested)
        {
            DateTimeOffset timeLimit = _timeProvider.GetDateTimeOffsetUTC().AddHours(-DelayInHours);
            await cleanupRateService.CleanupAsync(timeLimit, stoppingToken);

            await Task.Delay(TimeSpan.FromHours(DelayInHours), stoppingToken);
        }
    }
}