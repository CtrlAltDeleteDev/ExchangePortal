namespace Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

public class FetchExchangeRateQueryHandler : IRequestHandler<FetchExchangeRateQuery, Result<Models.ExchangeRate>>
{
    private readonly IDocumentStore _documentStore;
    
    public FetchExchangeRateQueryHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<Models.ExchangeRate>> Handle(FetchExchangeRateQuery request, CancellationToken cancellationToken)
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
        
        if (exchangeRateDocument is null)
        {
            ValidationException validation = new ValidationException(new []{new ValidationFailure("Rate", $"Pairs {request.SymbolFrom} <-> {request.SymbolTo} has no rates.")});
            return new Result<Models.ExchangeRate>(validation);
        }
        
        if (interestRateDocument is not null)
        {
            return ComposeExchangeRate(request, interestRateDocument, exchangeRateDocument);
        }
        
        return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, exchangeRateDocument.Price);
    }

    private static Result<Models.ExchangeRate> ComposeExchangeRate(FetchExchangeRateQuery request,
        InterestRateDocument interestRateDocument,
        ExchangeRateDocument exchangeRateDocument)
    {
        decimal amount = 0;
        if (interestRateDocument.Amount.HasValue)
        {
            amount = exchangeRateDocument.Price + interestRateDocument.Amount.Value;
            return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, amount);
        }

        amount = (decimal)((double)exchangeRateDocument.Price + (interestRateDocument.Percentage!.Value / 100.0) *
            (double)exchangeRateDocument.Price);

        return new Models.ExchangeRate(request.SymbolFrom, request.SymbolTo, amount);
    }
}