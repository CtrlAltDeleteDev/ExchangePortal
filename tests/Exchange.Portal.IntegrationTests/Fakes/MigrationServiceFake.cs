namespace Exchange.Portal.IntegrationTests.Fakes;

public class MigrationServiceFake : IMigrationService
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}