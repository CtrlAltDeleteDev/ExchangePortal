using Exchange.Portal.ApplicationCore.Models;

namespace Exchange.Portal.ApplicationCore.Configurations;

public class InitialTokensSettings
{
    public ICollection<Token> Tokens { get; set; } = new List<Token>();
}