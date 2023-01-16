namespace Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

public class ExchangeRateQueryValidator : AbstractValidator<FetchExchangeRateQuery>
{
    public ExchangeRateQueryValidator()
    {
        RuleFor(x => x.SymbolFrom)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(10);
        
        RuleFor(x => x.SymbolTo)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(10);
    }
}