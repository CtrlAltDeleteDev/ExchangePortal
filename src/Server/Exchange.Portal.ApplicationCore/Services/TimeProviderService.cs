using Exchange.Portal.ApplicationCore.Interface;

namespace Exchange.Portal.ApplicationCore.Services;

public class TimeProviderService : ITimeProviderService
{
    public DateTimeOffset GetDateTimeOffsetUTC() => DateTimeOffset.UtcNow;
}