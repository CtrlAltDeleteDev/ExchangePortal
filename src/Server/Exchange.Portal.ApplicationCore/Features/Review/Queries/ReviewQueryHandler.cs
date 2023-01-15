using LanguageExt.Common;
using Marten.Pagination;

namespace Exchange.Portal.ApplicationCore.Features.Review.Queries;

public class ReviewQueryHandler : IRequestHandler<ReviewQuery, Result<IReadOnlyCollection<Models.Review>>>
{
    private readonly IDocumentStore _documentStore;

    public ReviewQueryHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<IReadOnlyCollection<Models.Review>>> Handle(ReviewQuery request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session =_documentStore.LightweightSession();

        IPagedList<Models.Review> paginatedReviews = await session
            .Query<ReviewDocument>()
            .Where(x => x.IsReviewedByAdmin)
            .OrderByDescending(x => x.CreatedAt)
            .Select(x =>
                new Models.Review
                {
                    UserName = x.UserName,
                    UserEmail = x.UserEmail,
                    Text = x.Text,
                    CreatedAt = x.CreatedAt
                })
            .ToPagedListAsync(request.Pagination.Offset, request.Pagination.Count ,cancellationToken);

        return new Result<IReadOnlyCollection<Models.Review>>(paginatedReviews.ToList());
    }
}