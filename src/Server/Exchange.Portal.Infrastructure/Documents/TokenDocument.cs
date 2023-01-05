namespace Exchange.Portal.Infrastructure.Documents;

public sealed class TokenDocument
{
    public string Id { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}