using Exchange.Portal.ApplicationCore.Configurations;
using Exchange.Portal.ApplicationCore.Interface;
using Exchange.Portal.ApplicationCore.Models;
using Exchange.Portal.Infrastructure.Documents;
using Marten;

namespace Exchange.Portal.ApplicationCore.Services;

public class InitiateRateExchange : IInitiateRateExchange
{
    private readonly IDocumentStore _documentStore;
    private readonly InitialTokensSettings _tokes;
    
    public InitiateRateExchange(InitialTokensSettings tokes, IDocumentStore documentStore)
    {
        _tokes = tokes;
        _documentStore = documentStore;
    }

    public async Task InstantiateAsync()
    {
        if (!await _documentStore.QuerySession().Query<TokenDocument>().AnyAsync())
        {
            List<TokenDocument> documents = _tokes.Tokens
                .Select(x => new TokenDocument
                {
                    Id = Guid.NewGuid().ToString(),
                    Symbol = x.Symbol,
                    Name = x.Name
                })
                .ToList();

            await _documentStore.BulkInsertAsync(documents, BulkInsertMode.IgnoreDuplicates);
        }

        if (!await _documentStore.QuerySession().Query<PairDocument>().AnyAsync())
        {
            foreach (Token token in _tokes.Tokens)
            {
                PairDocument[] tokenPairs = _tokes.Tokens
                    .Where(x => x.Symbol != token.Symbol)
                    .Select(x => new PairDocument
                    {
                        Id = Guid.NewGuid().ToString(),
                        SymbolFrom = token.Symbol,
                        SymbolTo = x.Symbol,
                        Configuration = new PairConfiguration(null)
                    })
                    .ToArray();

                await _documentStore.BulkInsertAsync(tokenPairs);
            }
        }
    }
}