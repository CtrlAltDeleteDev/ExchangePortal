using TokenModel = Exchange.Portal.ApplicationCore.Models.Token;

namespace Exchange.Portal.ApplicationCore.Features.Token.Queries;

internal sealed class TokenQueryHandler : IRequestHandler<TokenQuery, IReadOnlyCollection<TokenModel>>
{
    private readonly IQuerySession _querySession;

    public TokenQueryHandler(IQuerySession querySession)
    {
        _querySession = querySession;
    }

    public async Task<IReadOnlyCollection<TokenModel>> Handle(TokenQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<TokenModel> result = await _querySession
            .Query<TokenDocument>()
            .Select(x => new TokenModel(x.Symbol, x.Name))
            .ToListAsync(cancellationToken);

        return result;
    }
}