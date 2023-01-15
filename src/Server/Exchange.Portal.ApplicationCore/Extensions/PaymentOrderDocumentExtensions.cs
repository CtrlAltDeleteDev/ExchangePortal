using Exchange.Portal.ApplicationCore.Features.Payment.Command;
using Exchange.Portal.ApplicationCore.Features.Payment.Command.Create;

namespace Exchange.Portal.ApplicationCore.Extensions;

public static class PaymentOrderDocumentExtensions
{
    public static PaymentOrderDocument ToDocument(this CreatePaymentOrderCommand createPaymentOrder)
    {
        return new PaymentOrderDocument
        {
            Id = Guid.NewGuid().ToString(),
            SymbolFrom = createPaymentOrder.SymbolFrom,
            AmountFrom = createPaymentOrder.AmountFrom,
            SymbolTo = createPaymentOrder.SymbolTo,
            AmountTo = createPaymentOrder.AmountTo,
            TransferWallet = createPaymentOrder.TransferWallet,
            ClientEmail = createPaymentOrder.ClientEmail,
            ClientWallet = createPaymentOrder.ClientWallet,
            CreatedAt = createPaymentOrder.CreatedAt,
            Status = OrderStatus.Created
        };
    } 
}