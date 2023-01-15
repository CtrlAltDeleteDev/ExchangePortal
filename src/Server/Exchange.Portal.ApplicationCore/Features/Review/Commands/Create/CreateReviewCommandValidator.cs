using FluentValidation;

namespace Exchange.Portal.ApplicationCore.Features.Review.Commands.Create;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommand>
{
    public CreateReviewCommandValidator()
    {
        RuleFor(x => x.UserName).NotEmpty();
        RuleFor(x => x.UserEmail).NotEmpty();
        RuleFor(x => x.Text).NotEmpty();
        
        RuleFor(x => x.CreatedAt)
            .GreaterThan(DateTimeOffset.UtcNow.AddSeconds(-10))
            .WithMessage("Data should not be in the past.");
    }
}