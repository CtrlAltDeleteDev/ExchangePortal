using Carter;
using Exchange.Portal.ApplicationCore.Features.Payment.Command;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Exchange.Portal.Presentation.Payment;

public class PaymentModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/payment", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync(PaymentRequest request, ISender sender)
    {
        var payment = new CreatePaymentCommand.Payment(request.Id, request.SymbolFrom, request.AmountFrom,
            request.SymbolTo, request.AmountTo, request.CreatedAt, request.TransferWallet, request.ClientEmail,
            request.ClientWallet);
        
        await sender.Send(payment);
        return Results.Ok();
    }
}

public record PaymentRequest(string Id, string SymbolFrom, decimal AmountFrom, string SymbolTo, decimal AmountTo,
    DateTimeOffset CreatedAt, string TransferWallet, string ClientEmail, string ClientWallet);