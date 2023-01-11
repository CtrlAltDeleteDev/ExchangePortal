namespace Exchange.Portal.Infrastructure.Documents;

public sealed class PairDocument
{
    public string Id { get; set; } = string.Empty;

    public string SymbolFrom { get; set; } = string.Empty;

    public string SymbolTo { get; set; } = string.Empty;
    
    public PairConfiguration Configuration { get; set; }
}

public class PairConfiguration
{
    public PairConfiguration(DateTimeOffset? lastRefresh = null, DateTimeOffset? nextRun = null, int refreshInterval = 60)
    {
        LastRefresh = lastRefresh;
        RefreshInterval = refreshInterval;
        NextRun = nextRun;
    }
    
    public DateTimeOffset? LastRefresh { get; set; }
    
    public DateTimeOffset? NextRun { get; set; }
    
    public int RefreshInterval { get; set; }
};