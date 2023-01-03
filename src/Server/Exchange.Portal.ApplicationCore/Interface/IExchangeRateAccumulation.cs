using Exchange.Portal.Infrastructure.Documents;

namespace Exchange.Portal.ApplicationCore.Interface;

public interface IExchangeRateAccumulation
{
    Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken);
}