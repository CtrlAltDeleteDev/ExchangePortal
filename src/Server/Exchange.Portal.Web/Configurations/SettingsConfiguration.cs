using Microsoft.Extensions.Options;

namespace Exchange.Portal.Web.Configurations;

internal static class SettingsConfiguration
{
    public static ApiConfigurationSettings AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<ApiConfigurationSettings>(configuration);
        
        services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<ApiConfigurationSettings>>()!.Value.InitialTokens);
        services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<ApiConfigurationSettings>>()!.Value.BinanceClient);
        services.AddScoped(cfg => cfg.GetService<IOptionsSnapshot<ApiConfigurationSettings>>()!.Value.ConnectionStrings);
        
        return configuration.Get<ApiConfigurationSettings>();
    }
}