namespace Exchange.Portal.ApplicationCore.Features.Token.Queries;

public record TokenQuery : IRequest<Result<IReadOnlyCollection<Models.Token>>>;