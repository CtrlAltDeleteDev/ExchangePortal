namespace Exchange.Portal.IntegrationTests;

internal sealed class TestApplicationFactory : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public TestApplicationFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(collection =>
        {
            collection.RemoveAll<IHostedService>();
            
            collection.Replace(new ServiceDescriptor(typeof(IMigrationService), typeof(MigrationServiceFake), ServiceLifetime.Scoped));

            collection.AddMarten(x => x.Connection(_connectionString));
        });
        
        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.AddInMemoryCollection(new Dictionary<string, string>
            {
                {"ConnectionStrings:DefaultConnection", _connectionString}
            }!);
        });
    }
}