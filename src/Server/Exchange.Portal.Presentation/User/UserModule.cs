namespace Exchange.Portal.Presentation.User;

internal sealed class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/login", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync(UserLoginRequest request, ISender sender)
    {
        var command = new LoginCommand(request.Login, request.Password);
        await sender.Send(command);
        return Results.Ok();
    }
}

public sealed record UserLoginRequest(string Login, string Password);