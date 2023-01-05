using System.Net;
using System.Text.Json;
using Exchange.Portal.ApplicationCore.Configurations;
using Exchange.Portal.ApplicationCore.Models;
using Exchange.Portal.Infrastructure.Documents;
using Marten;

namespace Exchange.Portal.ApplicationCore.Services;

internal class WebClientExchangeRateAccumulation : IExchangeRateAccumulation
{
    private readonly BinanceClientSettings _clientSettings;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IDocumentStore _documentStore;
    private readonly ITimeProviderService _providerService;

    public WebClientExchangeRateAccumulation(BinanceClientSettings clientSettings, 
        IHttpClientFactory httpClientFactory,
        IDocumentStore documentStore, 
        ITimeProviderService providerService)
    {
        _clientSettings = clientSettings;
        _httpClientFactory = httpClientFactory;
        _documentStore = documentStore;
        _providerService = providerService;
    }

    public async Task ExecuteAsync(IAsyncEnumerable<PairDocument> pairs, CancellationToken stoppingToken)
    {
        HttpClient client = _httpClientFactory.CreateClient();
        List<ExchangeRateDocument> rates = new List<ExchangeRateDocument>();
        await using var session = _documentStore.LightweightSession();

        await foreach (var pair in pairs.WithCancellation(stoppingToken))
        {
            HttpRequestMessage httpRequestMessage = new(
                HttpMethod.Get,
                _clientSettings.Url + pair.SymbolFrom.ToUpperInvariant() + pair.SymbolTo.ToUpperInvariant());

            var httpResponseMessage = await client.SendAsync(httpRequestMessage, stoppingToken);
            if (httpResponseMessage.StatusCode == HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(2000, stoppingToken);
                httpResponseMessage = await client.SendAsync(httpRequestMessage, stoppingToken);
            }

            if (!httpResponseMessage.IsSuccessStatusCode) continue;
            
            await using Stream contentStream =
                await httpResponseMessage.Content.ReadAsStreamAsync(stoppingToken);

            ClientRateResponse? result = await JsonSerializer.DeserializeAsync
                <ClientRateResponse>(contentStream, new JsonSerializerOptions(JsonSerializerDefaults.Web), stoppingToken);
                
            //direct rate
            rates.Add(new ExchangeRateDocument
            {
                Id = Guid.NewGuid().ToString(),
                SymbolFrom = pair.SymbolFrom,
                SymbolTo = pair.SymbolTo,
                Price = result!.Price,
                Timestamp = _providerService.GetDateTimeOffsetUTC()
            });
                
            //reverse rate
            rates.Add(new ExchangeRateDocument
            {
                Id = Guid.NewGuid().ToString(),
                SymbolFrom = pair.SymbolTo,
                SymbolTo = pair.SymbolFrom,
                Price = 1 / result.Price,
                Timestamp = _providerService.GetDateTimeOffsetUTC()
            });
                
            PairDocument reversedPair = await session
                .Query<PairDocument>()
                .Where(x => x.SymbolFrom == pair.SymbolTo && x.SymbolTo == pair.SymbolFrom)
                .FirstAsync(token: stoppingToken);
                
            pair.Configuration.LastRefresh = _providerService.GetDateTimeOffsetUTC();
            reversedPair.Configuration.LastRefresh = _providerService.GetDateTimeOffsetUTC();
                
            session.Store(pair, reversedPair);
            session.Store(rates.ToArray());
            await session.SaveChangesAsync(stoppingToken);

            await Task.Delay(500, stoppingToken);
        }
    }
}