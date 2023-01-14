using Exchange.Portal.ApplicationCore.Features.Review.Commands;
using Exchange.Portal.ApplicationCore.Features.Review.Queries;
using Exchange.Portal.Presentation.Common;
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Portal.Presentation.Review;

public sealed class ReviewModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/reviews", HandlerGetAsync);
        
        app.MapPost("api/review", async ([FromBody]CreateReviewRequest request, ISender sender) =>
        {
            ReviewCommand command = new (request.UserName, request.UserEmail, request.Text, request.CreatedAt);
            await sender.Send(command);

            return Results.Ok();
        });
    }
    
    private async Task<IResult> HandlerGetAsync(ISender sender, int offset = 0, int count = int.MaxValue)
    {
        ReviewQuery query = new(new OffsetPagination
        {
            Offset = offset,
            Count = count
        });
        IEnumerable<ApplicationCore.Models.Review> reviews = await sender.Send(query);

        return Results.Ok(reviews);
    }
}

public record CreateReviewRequest(string UserName, string UserEmail, string Text, DateTimeOffset CreatedAt);
