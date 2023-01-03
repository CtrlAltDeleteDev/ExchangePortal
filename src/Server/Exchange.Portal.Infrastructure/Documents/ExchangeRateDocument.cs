namespace Exchange.Portal.Infrastructure.Documents;

public sealed class ExchangeRateDocument
{
    public string Id { get; set; }
    
    public string SymbolFrom { get; set; }
    
    public string SymbolTo { get; set; }
    
    public decimal Price { get; set; }
    
    public DateTimeOffset Timestamp { get; set; }
}