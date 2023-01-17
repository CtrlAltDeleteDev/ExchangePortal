namespace Exchange.Portal.ApplicationCore.Features.InterestRate.Commands;


internal sealed class InterestRateCommandHandler : IRequestHandler<InterestRateCommand, Unit>
{
    private readonly IDocumentStore _documentStore;
    
    public InterestRateCommandHandler(IDocumentStore documentStore)
    {
        _documentStore = documentStore;
    }
    
    public async Task<Unit> Handle(InterestRateCommand request, CancellationToken cancellationToken)
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