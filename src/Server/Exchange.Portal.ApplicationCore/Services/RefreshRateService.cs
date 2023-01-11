namespace Exchange.Portal.ApplicationCore.Services;

internal class RefreshRateService : IRefreshRateService
{
    private readonly IEnumerable<IExchangeRateAccumulation> _exchangeRateAccumulationChain;
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _timeProvider;
    private readonly ILogger<RefreshRateService> _logger;

    public RefreshRateService(IDocumentStore documentStore,
        IEnumerable<IExchangeRateAccumulation> exchangeRateAccumulationChain, 
        ITimeProviderService timeProvider,
        ILogger<RefreshRateService> logger)
    {
        _documentStore = documentStore;
        _exchangeRateAccumulationChain = exchangeRateAccumulationChain;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async Task SyncRatesAsync(CancellationToken cancellationToken)
    {
        await using var session = _documentStore.LightweightSession();

        IQueryable<PairDocument> pairsQueryable = session.Query<PairDocument>()
            .Where(x => !x.Configuration.LastRefresh.HasValue ||
                        x.Configuration.NextRun < _timeProvider.GetDateTimeOffsetUTC());

        if (!await pairsQueryable.AnyAsync(cancellationToken))
        {
            _logger.LogDebug("No pair has been found to refresh");
            return;
        }
        
        IAsyncEnumerable<PairDocument> pairs = pairsQueryable
            .ToAsyncEnumerable(cancellationToken);
        
        foreach (IExchangeRateAccumulation exchangeRateAccumulation in _exchangeRateAccumulationChain)
        {
            _logger.LogDebug("Starting processing pairs");
            await exchangeRateAccumulation.ExecuteAsync(pairs, cancellationToken);
            _logger.LogDebug("Pairs have been processed");
        }
    }
}