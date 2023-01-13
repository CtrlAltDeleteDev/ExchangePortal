namespace Exchange.Portal.ApplicationCore.Interface;

public interface IExchangeRateAccumulator
{
    Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken);
}