namespace Exchange.Portal.IntegrationTests.Features;

[IntegrationTest]
[Collection(nameof(SliceFixture))]
public class GetTokensTests : BaseDocumentTest
{
    public GetTokensTests(SliceFixture sliceFixture) : base(sliceFixture)
    {
    }
    
    [Fact]
    public async Task Get_CallsGetTokens_ShouldReturnEmptyCollection()
    {
        // arrange
        await CleanCollectionAsync<TokenDocument>();
        
        // act
        (_, Token[]? response) = await GetAsync<Token[]>("api/tokes");
        
        // assert
        response.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Get_CallsGetTokens_ShouldReturnCollection()
    {
        // arrange
        await CleanCollectionAsync<TokenDocument>();
        await CleanCollectionAsync<TokenDocument>();
        TokenDocument[] tokenDocuments = Fixture.CreateMany<TokenDocument>(5).ToArray();
        InsetDocuments(tokenDocuments);
        Token[] expected = tokenDocuments.Select(x => new Token(x.Symbol, x.Name)).ToArray();
        
        // act
        (_, Token[]? response) = await GetAsync<Token[]>("api/tokes");
        
        // assert
        response.Should().BeEquivalentTo(expected);
    }
}