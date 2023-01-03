using Exchange.Portal.ApplicationCore.Interface;
using Exchange.Portal.Infrastructure.Documents;
using Marten;

namespace Exchange.Portal.ApplicationCore.Services;

public class TransferExchangeRateAccumulation : IExchangeRateAccumulation
{
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _timeProvider;

    private const string USDT = nameof(USDT);

    public TransferExchangeRateAccumulation(IDocumentStore documentStore, ITimeProviderService timeProvider)
    {
        _documentStore = documentStore;
        _timeProvider = timeProvider;
    }

    public async Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken)
    {
        await using var session = _documentStore.LightweightSession();
        await foreach (PairDocument pair in pairs.WithCancellation(stoppingToken))
        {
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
                Timestamp = _timeProvider.GetDateTimeOffsetUTC()
            };

            pair.Configuration.LastRefresh = _timeProvider.GetDateTimeOffsetUTC();
            
            session.Store(newTransferRate);
            session.Store(pair);
            await session.SaveChangesAsync(stoppingToken);
        }
    }
}