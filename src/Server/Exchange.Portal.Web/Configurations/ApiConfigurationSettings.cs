using Exchange.Portal.ApplicationCore.Configurations;

namespace Exchange.Portal.Web.Configurations;

internal class ApiConfigurationSettings
{
    public InitialTokensSettings InitialTokens { get; set; } = new ();
    
    public ConnectionStringsSettings ConnectionStrings { get; set; } = new();

    public BinanceClientSettings BinanceClient { get; set; } = new();

    public TelegramBotSettings TelegramBot { get; set; } = new();
}