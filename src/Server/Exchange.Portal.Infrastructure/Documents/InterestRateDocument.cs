namespace Exchange.Portal.Infrastructure.Documents;

public sealed class InterestRateDocument
{
    public string Id { get; set; } = string.Empty;
    
    public decimal? Amount { get; set; }
    
    public uint? Percentage { get; set; }
}