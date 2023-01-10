namespace Exchange.Portal.ApplicationCore.Features.Review.Commands;

public record ReviewCommand(string UserName, string UserEmail, string Text, DateTimeOffset CreatedAt) : IRequest<Unit>;