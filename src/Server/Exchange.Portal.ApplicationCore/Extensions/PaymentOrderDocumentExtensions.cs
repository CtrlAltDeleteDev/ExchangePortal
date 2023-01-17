using Exchange.Portal.ApplicationCore.Features.Payment.Command;

namespace Exchange.Portal.ApplicationCore.Extensions;

public static class PaymentOrderDocumentExtensions
{
    public static PaymentOrderDocument ToDocument(this CreatePayment.Payment payment)
    {
        return new PaymentOrderDocument
        {
            Id = Guid.NewGuid().ToString(),
            SymbolFrom = payment.SymbolFrom,
            AmountFrom = payment.AmountFrom,
            SymbolTo = payment.SymbolTo,
            AmountTo = payment.AmountTo,
            TransferWallet = payment.TransferWallet,
            ClientEmail = payment.ClientEmail,
            ClientWallet = payment.ClientWallet,
            CreatedAt = payment.CreatedAt,
            Status = OrderStatus.Created
        };
    } 
}