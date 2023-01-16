
using Microsoft.AspNetCore.Mvc;

namespace Exchange.Portal.Presentation.Review;

public sealed class ReviewModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/reviews", HandlerGetAsync);

        app.MapPost("api/review", async ([FromBody] CreateReviewRequest request, ISender sender) =>
        {
            CreateReviewCommand command = new(request.UserName, request.UserEmail, request.Text, request.CreatedAt);
            Result<Unit> result = await sender.Send(command);

            return result.ToOk();
        });
    }

    //TODO:Amend pagination
    private async Task<IResult> HandlerGetAsync(ISender sender, int offset = 1, int count = int.MaxValue)
    {
        ReviewQuery query = new(new OffsetPagination
        {
            Offset = offset,
            Count = count
        });

        Result<IReadOnlyCollection<ApplicationCore.Models.Review>> result = await sender.Send(query);

        return result.ToOk();
    }
}

public record CreateReviewRequest(string UserName, string UserEmail, string Text, DateTimeOffset CreatedAt);
