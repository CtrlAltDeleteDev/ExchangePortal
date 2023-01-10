namespace Exchange.Portal.ApplicationCore.Features.InterestRate.Commands;

public sealed record InterestRateCommand(decimal? Amount, uint? Percentage) : IRequest<Unit>;