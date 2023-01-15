using Exchange.Portal.ApplicationCore.Features.Payment.Command.Create;

namespace Exchange.Portal.Presentation.Payment;

internal sealed class PaymentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync(PaymentRequest request, ISender sender)
    {
        var payment = new CreatePaymentOrderCommand(request.Id, request.SymbolFrom, request.AmountFrom,
            request.SymbolTo, request.AmountTo, request.CreatedAt, request.TransferWallet, request.ClientEmail,
            request.ClientWallet);
        
        await sender.Send(payment);
        return Results.Ok();
    }
}

public sealed record PaymentRequest(string Id, string SymbolFrom, decimal AmountFrom, string SymbolTo, decimal AmountTo,
    DateTimeOffset CreatedAt, string TransferWallet, string ClientEmail, string ClientWallet);