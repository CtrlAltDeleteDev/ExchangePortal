using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.Payment.Command.Create;

public record CreatePaymentOrderCommand(string OrderId, string SymbolFrom, decimal AmountFrom, string SymbolTo, decimal AmountTo,
        DateTimeOffset CreatedAt, string TransferWallet, string ClientEmail, string ClientWallet) : IRequest<Result<Unit>>;