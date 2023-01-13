namespace Exchange.Portal.ApplicationCore.Services;

internal class CleanupRateService : ICleanupRateService
{
    private readonly IDocumentStore _documentStore;
    private readonly ILogger<CleanupRateService> _logger;

    public CleanupRateService(IDocumentStore documentStore, ILogger<CleanupRateService> logger)
    {
        _documentStore = documentStore;
        _logger = logger;
    }

    public async Task CleanupAsync(DateTimeOffset limitDateTimeOffset, CancellationToken cancellationToken = default)
    {
        _logger.LogDebug("Cleanup process starting");
        try
        {
            await using var session = _documentStore.LightweightSession();

            session
                .DeleteWhere<ExchangeRateDocument>(x => x.Timestamp < limitDateTimeOffset);

            await session.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogDebug(e, "Cleanup process has failed");
        }
    }
}