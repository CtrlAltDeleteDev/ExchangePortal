namespace Exchange.Portal.Infrastructure.Documents;

public sealed class ExchangeRateDocument
{
    public string Id { get; set; } = string.Empty;

    public string SymbolFrom { get; set; } = string.Empty;

    public string SymbolTo { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
}