namespace Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

public record ExchangeRateQuery(string SymbolFrom, string SymbolTo) : IRequest<Models.ExchangeRate>;