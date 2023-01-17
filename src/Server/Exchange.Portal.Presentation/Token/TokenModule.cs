namespace Exchange.Portal.Presentation.Token;

internal sealed class TokenModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/tokes", HandlerAsync);
    }
    
    private async Task<IResult> HandlerAsync(ISender sender)
    {
        TokenQuery tokenQuery = new ();
        
        //TODO: map and return response.
        IReadOnlyCollection<ApplicationCore.Models.Token> result = await sender.Send(tokenQuery);
        
        return Results.Ok(result);
    }
}