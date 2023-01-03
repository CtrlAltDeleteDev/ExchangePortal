namespace Exchange.Portal.ApplicationCore.Interface;

public interface IRefreshRateService
{
    Task SyncRatesAsync(CancellationToken cancellationToken);
}