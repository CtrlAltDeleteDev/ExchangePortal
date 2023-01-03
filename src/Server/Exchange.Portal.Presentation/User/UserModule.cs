using Carter;
using Exchange.Portal.ApplicationCore.Features.User.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Exchange.Portal.Presentation.User;

public class UserModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync(UserLoginRequest request, ISender sender)
    {
        SignInCommand.User user = new SignInCommand.User(request.Login, request.Password);
        await sender.Send(user);
        return Results.Ok();
    }
}

public sealed record UserLoginRequest(string Login, string Password);