using FluentValidation;

namespace Exchange.Portal.ApplicationCore.Features.InterestRate.Commands;

public class CreateUpdateInterestRateCommandValidator : AbstractValidator<CreateUpdateInterestRateCommand>
{
    public CreateUpdateInterestRateCommandValidator()
    {
        RuleFor(x => x)
            .Must(x => x.Amount is not null || x.Percentage is not null)
            .WithMessage("One param is mandatory. Enter either 'Amount' or 'Percentage'.");

        RuleFor(x => x.Amount)
            .GreaterThan(0);

        RuleFor(x => x.Percentage)
            .GreaterThan((uint)0);
    }
}