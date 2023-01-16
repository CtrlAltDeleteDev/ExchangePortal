namespace Exchange.Portal.ApplicationCore.Features.Payment.Command.Create;

public class CreatePaymentOrderCommandValidator : AbstractValidator<CreatePaymentOrderCommand>
{
    public CreatePaymentOrderCommandValidator()
    {
        RuleFor(x => x.OrderId).NotEmpty();
        RuleFor(x => x.AmountFrom).GreaterThan(0);
        RuleFor(x => x.AmountTo).GreaterThan(0);
        RuleFor(x => x.ClientEmail).NotEmpty();
        RuleFor(x => x.ClientWallet).NotEmpty();
        RuleFor(x => x.TransferWallet).NotEmpty();
        
        RuleFor(x => x.CreatedAt)
            .GreaterThan(DateTimeOffset.UtcNow.AddSeconds(-10))
            .WithMessage("Data should not be in the past.");
        
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