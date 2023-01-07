namespace Exchange.Portal.ApplicationCore.Consts;

internal static class TelegramPaymentMessageTemplate
{
    public const string OrderId = "**Order id:** {0}";
    
    public const string ClientSymbolFrom = "**Currency from:** {0}";
    
    public const string ClientSymbolTo = "**Currency to:** {0}";
    
    public const string ClientAmountTo = "**Amount to exchange to:** {0}";
    
    public const string ClientAmountFrom = "**Amount to exchange from:** {0}";

    public const string ClientWallet = "**Client wallet:** {0}";
    
    public const string ClientEmail = "**Client email:** {0}";
    
    public const string TransferWallet = "**Transfer wallet:** {0}";
    
    public const string ExchangeRate = "**Exchange rate:** {0}";
    
    public const string OrderCreatedAt = "**Order created at:** {0}";
}