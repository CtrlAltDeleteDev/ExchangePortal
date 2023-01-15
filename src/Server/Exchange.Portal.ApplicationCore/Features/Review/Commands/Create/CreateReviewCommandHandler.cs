using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.Review.Commands.Create;

internal class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Result<Unit>>
{
    private readonly IDocumentStore _documentStore;

    public CreateReviewCommandHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<Unit>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
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
