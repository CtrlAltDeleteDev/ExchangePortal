namespace Exchange.Portal.ApplicationCore.Features.Review.Commands;


public class ReviewCommandHandler : IRequestHandler<ReviewCommand, Unit>
{
    private readonly IDocumentStore _documentStore;

    public ReviewCommandHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Unit> Handle(ReviewCommand request, CancellationToken cancellationToken)
    {
        await using var session = _documentStore.LightweightSession();

        session.Store(new ReviewDocument
        {
            Id = Guid.NewGuid().ToString(),
            UserName = request.UserName,
            UserEmail = request.UserEmail,
            Text = request.Text,
            CreatedAt = request.CreatedAt,
            IsReviewedByAdmin = false
        });

        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
