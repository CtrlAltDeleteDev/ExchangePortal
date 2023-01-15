using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

public record FetchExchangeRateQuery(string SymbolFrom, string SymbolTo) : IRequest<Result<Models.ExchangeRate>>;