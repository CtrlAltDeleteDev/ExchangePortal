using Exchange.Portal.ApplicationCore.Features.Review.Commands;
using Exchange.Portal.ApplicationCore.Features.Review.Commands.Create;
using Exchange.Portal.ApplicationCore.Features.Review.Queries;
using Exchange.Portal.Presentation.Common;
using LanguageExt.Common;

namespace Exchange.Portal.Presentation.Review;

internal sealed class ReviewModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/reviews", async (GetReviewRequest request, ISender sender) =>
        {
            ReviewQuery query = new (request.Pagination);
            Result<IReadOnlyCollection<ApplicationCore.Models.Review>> reviews = await sender.Send(query);

            return Results.Ok(reviews);
        });
        
        app.MapPost("api/review", async (CreateReviewRequest request, ISender sender) =>
        {
            CreateReviewCommand command = new (request.UserName, request.UserEmail, request.Text, request.CreatedAt);
            await sender.Send(command);

            return Results.Ok();
        });
    }
}

public record GetReviewRequest(OffsetPagination Pagination);

public record CreateReviewRequest(string UserName, string UserEmail, string Text, DateTimeOffset CreatedAt);
