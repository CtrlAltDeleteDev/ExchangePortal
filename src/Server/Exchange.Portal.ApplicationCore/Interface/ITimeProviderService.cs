namespace Exchange.Portal.ApplicationCore.Interface;

public interface ITimeProviderService
{
    DateTimeOffset GetDateTimeOffsetUTC();
}