using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.InterestRate.Commands;

public sealed record CreateUpdateInterestRateCommand(decimal? Amount, uint? Percentage) : IRequest<Result<Unit>>;