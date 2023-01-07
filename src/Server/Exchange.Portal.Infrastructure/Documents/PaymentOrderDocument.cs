namespace Exchange.Portal.Infrastructure.Documents;

public sealed class PaymentOrderDocument
{
    public string Id { get; set; }
    
    public string SymbolFrom { get; set; } = string.Empty;
    
    public decimal AmountFrom { get; set; }

    public string SymbolTo { get; set; } = string.Empty;
    
    public decimal AmountTo { get; set; }

    public string TransferWallet { get; set; } = string.Empty;
    
    public string ClientEmail { get; set; } = string.Empty;
    
    public string ClientWallet { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public OrderStatus Status { get; set; }
}

public enum OrderStatus
{
    Created,
    
}