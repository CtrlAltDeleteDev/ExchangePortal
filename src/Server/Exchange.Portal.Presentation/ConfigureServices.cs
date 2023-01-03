using Carter;
using Microsoft.Extensions.DependencyInjection;

namespace Exchange.Portal.Presentation;

public static class ConfigureServices
{
    public static IServiceCollection AddPresentation(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddCarter();
        
        return serviceCollection;
    }
}