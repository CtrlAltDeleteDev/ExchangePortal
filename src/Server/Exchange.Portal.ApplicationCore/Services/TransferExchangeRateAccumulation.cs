namespace Exchange.Portal.ApplicationCore.Services;

internal class TransferExchangeRateAccumulation : IExchangeRateAccumulation
{
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _timeProvider;
    private readonly ILogger<TransferExchangeRateAccumulation> _logger;

    private const string USDT = nameof(USDT);

    public TransferExchangeRateAccumulation(IDocumentStore documentStore, 
        ITimeProviderService timeProvider,
        ILogger<TransferExchangeRateAccumulation> logger)
    {
        _documentStore = documentStore;
        _timeProvider = timeProvider;
        _logger = logger;
    }

    public async Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken)
    {
        await using var session = _documentStore.LightweightSession();
        await foreach (PairDocument pair in pairs.WithCancellation(stoppingToken))
        {
            DateTimeOffset dateTimeOffsetUtc = _timeProvider.GetDateTimeOffsetUTC();
            
            ExchangeRateDocument fromTokenIntoUSDT = await session
                .Query<ExchangeRateDocument>()
                .Where(x => x.SymbolFrom == pair.SymbolFrom && x.SymbolTo == USDT)
                .FirstAsync(token: stoppingToken);
            
            ExchangeRateDocument toTokenIntoUSDT = await session
                .Query<ExchangeRateDocument>()
                .Where(x => x.SymbolFrom == pair.SymbolTo && x.SymbolTo == USDT)
                .FirstAsync(token: stoppingToken);

            ExchangeRateDocument newTransferRate = new()
            {
                Id = Guid.NewGuid().ToString(),
                SymbolFrom = pair.SymbolFrom,
                SymbolTo = pair.SymbolTo,
                Price = fromTokenIntoUSDT.Price / toTokenIntoUSDT.Price,
                Timestamp = dateTimeOffsetUtc
            };

            pair.Configuration.LastRefresh = dateTimeOffsetUtc;
            pair.Configuration.NextRun = dateTimeOffsetUtc.AddMinutes(pair.Configuration.RefreshInterval);
            
            session.Store(newTransferRate);
            session.Store(pair);
            await session.SaveChangesAsync(stoppingToken);

            _logger.LogDebug("Pair with {SymbolFrom} <-> {SymbolTo} has been processed", pair.SymbolFrom,
                pair.SymbolTo);
        }
    }
}