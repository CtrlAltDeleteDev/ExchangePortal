namespace Exchange.Portal.IntegrationTests.Handlers;

[IntegrationTest]
[Collection(nameof(SliceFixture))]
public class TokenQueryHandlerDocumentTests : BaseDocumentTest
{
    public TokenQueryHandlerDocumentTests(SliceFixture sliceFixture) : base(sliceFixture)
    {
    }

    [Fact]
    public async Task Handler_NoItemsInDb_ShouldReturnEmptyCollection()
    {
        await CleanCollectionAsync<TokenDocument>();
        TokenQuery tokenQuery = new();

        IReadOnlyCollection<Token> actual = await Mediator.Send(tokenQuery);

        actual.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Handler_ExistingItemsInDb_ShouldReturnEmptyCollection()
    {
        await CleanCollectionAsync<TokenDocument>();
        TokenDocument[] tokenDocuments = Fixture.CreateMany<TokenDocument>(5).ToArray();
        InsetDocuments(tokenDocuments);
        Token[] expected = tokenDocuments.Select(x => new Token(x.Symbol, x.Name)).ToArray();
        TokenQuery tokenQuery = new();

        IReadOnlyCollection<Token> actual = await Mediator.Send(tokenQuery);

        actual.Should().BeEquivalentTo(expected);
    }
}