namespace Exchange.Portal.IntegrationTests;

[CollectionDefinition(nameof(SliceFixture))]
public class SliceFixtureCollection : ICollectionFixture<SliceFixture> { }

public sealed class SliceFixture : IAsyncLifetime
{
    private TestApplicationFactory _factory;
    private readonly PostgreSqlTestcontainer _testContainer;

    public SliceFixture()
    {
        ITestcontainersBuilder<PostgreSqlTestcontainer>? testContainerBuilder = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithCleanUp(true)
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "postgres",
                Username = "postgres",
                Password = "postgres"
            })
            .WithImage("clkao/postgres-plv8"); // use a different base image including plv8 plugin
        
        _testContainer = testContainerBuilder.Build();
    }
    
    public async Task InitializeAsync()
    {
        await _testContainer.StartAsync();
        
        _factory = new TestApplicationFactory(_testContainer.ConnectionString);
    }
    
    public T GetScoped<T>() where T: class
    {
       return _factory.Services.CreateScope().ServiceProvider.GetRequiredService<T>();
    }

    public HttpClient CreateClient() => _factory.CreateClient();
    
    public async Task DisposeAsync()
    {
        await _testContainer.StopAsync();
        await _testContainer.DisposeAsync();
    }
}