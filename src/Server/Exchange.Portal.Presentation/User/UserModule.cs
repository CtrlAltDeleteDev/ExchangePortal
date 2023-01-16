using Microsoft.AspNetCore.Mvc;

namespace Exchange.Portal.Presentation.User;

public sealed class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/login", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync([FromBody] UserLoginRequest request, ISender sender)
    {
        LoginCommand command = new(request.Login, request.Password);

        Result<Unit> result = await sender.Send(command);

        return result.ToOk();
    }
}

public sealed record UserLoginRequest(string Login, string Password);