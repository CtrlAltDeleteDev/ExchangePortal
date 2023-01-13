namespace Exchange.Portal.IntegrationTests.Abstractions;

public abstract class BaseDocumentTest
{
    protected readonly SliceFixture SliceFixture;
    protected readonly Fixture Fixture;

    protected BaseDocumentTest(SliceFixture sliceFixture)
    {
        SliceFixture = sliceFixture;
        Fixture = new Fixture();
    }

    protected IMediator Mediator => SliceFixture.GetScoped<IMediator>();

    protected IDocumentStore DocumentStore => SliceFixture.GetScoped<IDocumentStore>();
    
    protected void InsetDocuments<T>(IReadOnlyCollection<T> items)
    {
        DocumentStore.BulkInsert(items);
    }

    protected async Task CleanCollectionAsync<T>()
    {
        await DocumentStore.Advanced.Clean.DeleteDocumentsByTypeAsync(typeof(T));
    }
}