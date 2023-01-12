namespace Exchange.Portal.ApplicationCore.Interface;

public interface ICleanupRateService
{
    Task CleanupAsync(DateTimeOffset limitDateTimeOffset, CancellationToken cancellationToken = default);
}