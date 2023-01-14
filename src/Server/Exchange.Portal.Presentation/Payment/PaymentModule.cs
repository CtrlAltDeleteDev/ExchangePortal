using Microsoft.AspNetCore.Mvc;

namespace Exchange.Portal.Presentation.Payment;

public sealed class PaymentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync([FromBody]PaymentRequest request, ISender sender)
    {
        var payment = new CreatePayment.Payment(request.Id, request.SymbolFrom, request.AmountFrom,
            request.SymbolTo, request.AmountTo, request.CreatedAt, request.TransferWallet, request.ClientEmail,
            request.ClientWallet);
        
        await sender.Send(payment);
        return Results.Ok();
    }
}

public sealed record PaymentRequest(string Id, string SymbolFrom, decimal AmountFrom, string SymbolTo, decimal AmountTo,
    DateTimeOffset CreatedAt, string TransferWallet, string ClientEmail, string ClientWallet);