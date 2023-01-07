using Exchange.Portal.Infrastructure.Documents;
using Marten;
using Microsoft.AspNetCore.Identity;
using Weasel.Core;

namespace Exchange.Portal.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection, string connectionString)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));

        serviceCollection.AddIdentity<IdentityUser, IdentityRole>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        
        serviceCollection.AddMarten(options =>
        {
            options.Connection(connectionString);
            
            options.AutoCreateSchemaObjects = AutoCreate.All;

            options.RegisterDocumentType<TokenDocument>();
            options.RegisterDocumentType<PairDocument>();
            options.RegisterDocumentType<ExchangeRateDocument>();
            
        }).ApplyAllDatabaseChangesOnStartup();
        
        return serviceCollection;
    }
}