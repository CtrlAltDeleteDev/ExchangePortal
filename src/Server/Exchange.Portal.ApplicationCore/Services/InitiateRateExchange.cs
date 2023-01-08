using Exchange.Portal.ApplicationCore.Configurations;
using Exchange.Portal.ApplicationCore.Models;
using Exchange.Portal.Infrastructure.Documents;
using Marten;
using Microsoft.Extensions.Logging;

namespace Exchange.Portal.ApplicationCore.Services;

internal class InitiateRateExchange : IInitiateRateExchange
{
    private readonly IDocumentStore _documentStore;
    private readonly InitialTokensSettings _tokes;
    private readonly ILogger<InitiateRateExchange> _logger;


    public InitiateRateExchange(InitialTokensSettings tokes, 
        IDocumentStore documentStore,
        ILogger<InitiateRateExchange> logger)
    {
        _tokes = tokes;
        _documentStore = documentStore;
        _logger = logger;
    }

    public async Task InstantiateAsync()
    {
        if (!await _documentStore.QuerySession().Query<TokenDocument>().AnyAsync())
        {
            _logger.LogInformation("Initiate all the tokens");
            
            List<TokenDocument> documents = _tokes.Tokens
                .Select(x => new TokenDocument
                {
                    Id = Guid.NewGuid().ToString(),
                    Symbol = x.Symbol,
                    Name = x.Name
                })
                .ToList();

            await _documentStore.BulkInsertAsync(documents, BulkInsertMode.IgnoreDuplicates);
            
            _logger.LogInformation("All the tokens have been added");
        }

        if (!await _documentStore.QuerySession().Query<PairDocument>().AnyAsync())
        {
            _logger.LogInformation("Creating pairs for all tokens");

            foreach (string symbol in _tokes.Tokens.Select(token => token.Symbol ))
            {
                PairDocument[] tokenPairs = _tokes.Tokens
                    .Where(x => x.Symbol != symbol)
                    .Select(x => new PairDocument
                    {
                        Id = Guid.NewGuid().ToString(),
                        SymbolFrom = symbol,
                        SymbolTo = x.Symbol,
                        Configuration = new PairConfiguration(null)
                    })
                    .ToArray();

                await _documentStore.BulkInsertAsync(tokenPairs);
                
                _logger.LogInformation("Created pairs for all tokens");
            }
        }
    }
}