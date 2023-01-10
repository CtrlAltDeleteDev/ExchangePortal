namespace Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

public class ExchangeRateQueryHandler : IRequestHandler<ExchangeRateQuery, Models.ExchangeRate>
{
    private readonly IDocumentStore _documentStore;
    
    public ExchangeRateQueryHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Models.ExchangeRate> Handle(ExchangeRateQuery request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _documentStore.LightweightSession();
        
        InterestRateDocument? interestRateDocument = await session
            .Query<InterestRateDocument>()
            .SingleOrDefaultAsync(cancellationToken);

        ExchangeRateDocument? exchangeRateDocument = await session
            .Query<ExchangeRateDocument>()
            .Where(x => x.SymbolFrom == request.SymbolFrom && x.SymbolTo == request.SymbolTo)
            .OrderByDescending(x => x.Timestamp)
            .FirstOrDefaultAsync(cancellationToken);

        //TODO: Add a proper validation
        if (exchangeRateDocument is null)
        {
            throw new ArgumentException($"Exchange rate for {request.SymbolFrom} <-> {request.SymbolTo} is not found");
        }
        
        if (interestRateDocument is not null)
        {
            if (interestRateDocument.Amount.HasValue)
            {
                decimal amount = exchangeRateDocument.Price + interestRateDocument.Amount.Value;
                return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, amount);
            }

            if (interestRateDocument.Percentage.HasValue)
            {
                double amount = (double)exchangeRateDocument.Price + (interestRateDocument.Percentage.Value / 100.0) *
                    (double)exchangeRateDocument.Price;
                return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, (decimal)amount);
            }
        }
        
        return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, exchangeRateDocument.Price);
    }
}