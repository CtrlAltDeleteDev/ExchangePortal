namespace Exchange.Portal.ApplicationCore.Services;

internal class TimeProviderService : ITimeProviderService
{
    public DateTimeOffset GetDateTimeOffsetUTC() => DateTimeOffset.UtcNow;
}