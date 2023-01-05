using Exchange.Portal.Infrastructure.Documents;
using Marten;

namespace Exchange.Portal.ApplicationCore.Services;

internal class RefreshRateService : IRefreshRateService
{
    private readonly IEnumerable<IExchangeRateAccumulation> _exchangeRateAccumulationChain;
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _timeProvider;

    public RefreshRateService(IDocumentStore documentStore, IEnumerable<IExchangeRateAccumulation> exchangeRateAccumulationChain, ITimeProviderService timeProvider)
    {
        _documentStore = documentStore;
        _exchangeRateAccumulationChain = exchangeRateAccumulationChain;
        _timeProvider = timeProvider;
    }

    public async Task SyncRatesAsync(CancellationToken cancellationToken)
    {
        await using var session = _documentStore.LightweightSession();

        IAsyncEnumerable<PairDocument> pairs = session.Query<PairDocument>()
            .Where(x => !x.Configuration.LastRefresh.HasValue ||
                        x.Configuration.LastRefresh.Value.AddMinutes(x.Configuration.RefreshInterval) <
                        _timeProvider.GetDateTimeOffsetUTC())
            .ToAsyncEnumerable(cancellationToken);

        foreach (var exchangeRateAccumulation in _exchangeRateAccumulationChain)
        {
            await exchangeRateAccumulation.ExecuteAsync(pairs, cancellationToken);
        }
    }
}