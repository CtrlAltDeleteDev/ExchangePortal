namespace Exchange.Portal.Presentation.Token;

public sealed class TokenModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tokens", HandlerAsync);
    }

    private async Task<IResult> HandlerAsync(ISender sender)
    {
        TokenQuery tokenQuery = new();

        Result<IReadOnlyCollection<ApplicationCore.Models.Token>> result = await sender.Send(tokenQuery);

        return result.ToOk();
    }
}