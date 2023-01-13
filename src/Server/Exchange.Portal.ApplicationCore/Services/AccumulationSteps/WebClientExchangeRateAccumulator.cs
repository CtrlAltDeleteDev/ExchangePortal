using Exchange.Portal.ApplicationCore.HttpClients;
using Refit;

namespace Exchange.Portal.ApplicationCore.Services.AccumulationSteps;

internal class WebClientExchangeRateAccumulator : IExchangeRateAccumulator
{
    private readonly IBinanceClient _binanceClient;
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _providerService;
    private readonly ILogger<WebClientExchangeRateAccumulator> _logger;

    public WebClientExchangeRateAccumulator(
        IDocumentStore documentStore,
        ITimeProviderService providerService,
        ILogger<WebClientExchangeRateAccumulator> logger, IBinanceClient binanceClient)
    {
        _documentStore = documentStore;
        _providerService = providerService;
        _logger = logger;
        _binanceClient = binanceClient;
    }

    public async Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken)
    {
        List<ExchangeRateDocument> rates = new List<ExchangeRateDocument>();
        await using var session = _documentStore.LightweightSession();

        await foreach (var pair in pairs.WithCancellation(stoppingToken))
        {
            ApiResponse<RateResponse> result = await _binanceClient.GetRateAsync(pair.SymbolFrom.ToUpperInvariant(),
                pair.SymbolTo.ToUpperInvariant());
            
            if (!result.IsSuccessStatusCode)
            {
                _logger.LogDebug("Pair with {SymbolFrom} <-> {SymbolTo} has not been found", pair.SymbolFrom,
                    pair.SymbolTo);
                continue;
            }

            //direct rate
            rates.Add(new ExchangeRateDocument
            {
                Id = Guid.NewGuid().ToString(),
                SymbolFrom = pair.SymbolFrom,
                SymbolTo = pair.SymbolTo,
                Price = result.Content.Price,
                Timestamp = _providerService.GetDateTimeOffsetUTC()
            });

            //reverse rate
            rates.Add(new ExchangeRateDocument
            {
                Id = Guid.NewGuid().ToString(),
                SymbolFrom = pair.SymbolTo,
                SymbolTo = pair.SymbolFrom,
                Price = 1 / result.Content.Price,
                Timestamp = _providerService.GetDateTimeOffsetUTC()
            });

            PairDocument reversedPair = await session
                .Query<PairDocument>()
                .Where(x => x.SymbolFrom == pair.SymbolTo && x.SymbolTo == pair.SymbolFrom)
                .FirstAsync(token: stoppingToken);

            DateTimeOffset nextRun = _providerService.GetDateTimeOffsetUTC().AddMinutes(pair.Configuration.RefreshInterval);
            DateTimeOffset lastRefresh = _providerService.GetDateTimeOffsetUTC();

            pair.Configuration.LastRefresh = lastRefresh;
            pair.Configuration.NextRun = nextRun;

            reversedPair.Configuration.LastRefresh = nextRun;
            reversedPair.Configuration.NextRun = nextRun;

            session.Store(pair, reversedPair);
            session.Store(rates.ToArray());
            await session.SaveChangesAsync(stoppingToken);

            _logger.LogDebug("Pair with {SymbolFrom} <-> {SymbolTo} has been processed", pair.SymbolFrom,
                pair.SymbolTo);

            await Task.Delay(500, stoppingToken);
        }
    }
}