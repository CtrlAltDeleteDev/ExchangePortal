namespace Exchange.Portal.ApplicationCore.Features.InterestRate.Commands;


internal sealed class
    CreateUpdateInterestRateCommandHandler : IRequestHandler<CreateUpdateInterestRateCommand, Result<Unit>>
{
    private readonly IDocumentStore _documentStore;

    public CreateUpdateInterestRateCommandHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }

    public async Task<Result<Unit>> Handle(CreateUpdateInterestRateCommand request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _documentStore.LightweightSession();

        InterestRateDocument? existingInterestRate = await session
            .Query<InterestRateDocument>()
            .FirstOrDefaultAsync(cancellationToken);

        InterestRateDocument interestRate;
        if (existingInterestRate is null)
        {
            interestRate = new InterestRateDocument
            {
                Id = Guid.NewGuid().ToString()
            };
        }
        else
        {
            interestRate = existingInterestRate;
        }

        interestRate.Amount = request.Amount;
        interestRate.Percentage = request.Percentage;

        session.Store(interestRate);
        await session.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}