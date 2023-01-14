namespace Exchange.Portal.IntegrationTests.Features;

[IntegrationTest]
[Collection(nameof(SliceFixture))]
public class GetReviewsTests : BaseDocumentTest
{
    public GetReviewsTests(SliceFixture sliceFixture) : base(sliceFixture)
    {
    }

    [Fact]
    public async Task Get_CallsGetTokens_ShouldReturnEmptyCollection()
    {
        // arrange
        await CleanCollectionAsync<ReviewDocument>();
        KeyValuePair<string, object>[] queryParam =
        {
            new("offset", 1),
            new("count", 10)
        };

        // act
        (_, ReviewDocument[]? response) = await GetAsync<ReviewDocument[]>("api/reviews", queryParam);

        // assert
        response.Should().BeEmpty();
    }
}