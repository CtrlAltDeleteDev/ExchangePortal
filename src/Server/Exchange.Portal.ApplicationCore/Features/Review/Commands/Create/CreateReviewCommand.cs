namespace Exchange.Portal.ApplicationCore.Features.Review.Commands.Create;

public record CreateReviewCommand(string UserName, string UserEmail, string Text, DateTimeOffset CreatedAt) 
    : IRequest<Result<Unit>>;