namespace Exchange.Portal.ApplicationCore.Features.Token.Queries;

public record TokenQuery : IRequest<IReadOnlyCollection<Models.Token>>;