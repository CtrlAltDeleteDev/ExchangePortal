using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Text;
using System.Text.Json;

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

    protected HttpClient Client => SliceFixture.CreateClient();
    
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

    protected async Task<(bool successuful, T? response)> GetAsync<T>(
        [StringSyntax(StringSyntaxAttribute.Uri)] string? requestUri,
        KeyValuePair<string, object>[]? queryParams = null,
        bool assertIfNotOk = false)
    {
        var sb = new StringBuilder();
        if (queryParams is not null)
        {
            sb.Append("?");
            foreach (var queryParam in queryParams)
            {
                sb.AppendFormat("{0}={1}&", queryParam.Key, queryParam.Value);
            }

            sb.Length--;
        }
        
        HttpResponseMessage response = await Client.GetAsync(requestUri + sb);

        if (assertIfNotOk)
        {
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        await using Stream stream = await response.Content.ReadAsStreamAsync();
        T? result = JsonSerializer.Deserialize<T>(stream, new JsonSerializerOptions(JsonSerializerDefaults.Web));
        return (true, result);
    }
}