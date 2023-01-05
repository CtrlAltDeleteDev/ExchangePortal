namespace Exchange.Portal.Infrastructure.Documents;

public class PairDocument
{
    public string Id { get; set; } = string.Empty;

    public string SymbolFrom { get; set; } = string.Empty;

    public string SymbolTo { get; set; } = string.Empty;

    public PairConfiguration Configuration { get; set; } = new(null);
}

public class PairConfiguration
{
    public PairConfiguration(DateTimeOffset? lastRefresh, int refreshInterval = 60)
    {
        LastRefresh = lastRefresh;
        RefreshInterval = refreshInterval;
    }
    
    public DateTimeOffset? LastRefresh { get; set; }
    
    public int RefreshInterval { get; set; }
};