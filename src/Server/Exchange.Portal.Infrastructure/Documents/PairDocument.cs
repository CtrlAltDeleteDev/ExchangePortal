namespace Exchange.Portal.Infrastructure.Documents;

public class PairDocument
{
    public string Id { get; set; }
    
    public string SymbolFrom { get; set; }
    
    public string SymbolTo { get; set; }
    
    public PairConfiguration Configuration { get; set; }
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