using FluentValidation;
using FluentValidation.Results;
using LanguageExt.Common;

namespace Exchange.Portal.ApplicationCore.Features.Payment.Command.Create;

internal sealed class CreatePaymentOrderCommandHandler : IRequestHandler<CreatePaymentOrderCommand, Result<Unit>>
{
    private readonly ITelegramBotClient _telegramBotClient;
    private readonly TelegramBotSettings _botSettings;
    private readonly IDocumentStore _documentStore;
    private readonly ILogger<CreatePaymentOrderCommandHandler> _logger;

    public CreatePaymentOrderCommandHandler(ITelegramBotClient telegramBotClient,
        TelegramBotSettings botSettings,
        IDocumentStore documentStore,
        ILogger<CreatePaymentOrderCommandHandler> logger)
    {
        _telegramBotClient = telegramBotClient;
        _botSettings = botSettings;
        _documentStore = documentStore;
        _logger = logger;
    }

    public async Task<Result<Unit>> Handle(CreatePaymentOrderCommand request, CancellationToken cancellationToken)
    {
        await using IDocumentSession session = _documentStore.LightweightSession();

        ValidationResult validationResult = await ValidateAsync(session, request);
        if (!validationResult.IsValid)
        {
            ValidationException validation = new ValidationException(validationResult.Errors);
            return new Result<Unit>(validation);
        }

        ExchangeRateDocument exchangeRate = await session.Query<ExchangeRateDocument>()
            .Where(x => x.SymbolFrom == request.SymbolFrom && x.SymbolTo == request.SymbolTo)
            .FirstAsync(cancellationToken);

        PaymentOrderDocument document = request.ToDocument();
        session.Store(document);
        Task saveChangesTask = session.SaveChangesAsync(cancellationToken);

        string message = ConstructMessage(request, exchangeRate);
        Task telegramSendTask = _telegramBotClient.SendTextMessageAsync(_botSettings.ChatId, message,
            cancellationToken: cancellationToken);

        await Task.WhenAll(saveChangesTask, telegramSendTask);

        _logger.LogInformation("Payment order is created, telegram message has been sent");

        return Unit.Value;
    }

    private async Task<ValidationResult> ValidateAsync(IDocumentSession session, CreatePaymentOrderCommand request)
    {
        List<ValidationFailure> errors = new();

        bool hasSymbolFromToken =
            await session.Query<TokenDocument>().Where(x => x.Symbol == request.SymbolFrom).AnyAsync();
        if (!hasSymbolFromToken)
        {
            errors.Add(new ValidationFailure(nameof(request.SymbolFrom), $"{request.SymbolFrom} is not found."));
        }

        bool hasSymbolToToken =
            await session.Query<TokenDocument>().Where(x => x.Symbol == request.SymbolTo).AnyAsync();
        if (!hasSymbolToToken)
        {
            errors.Add(new ValidationFailure(nameof(request.SymbolTo), $"{request.SymbolTo} is not found."));
        }

        bool hasPairRate = await session.Query<ExchangeRateDocument>()
            .Where(x => x.SymbolFrom == request.SymbolFrom && x.SymbolTo == request.SymbolTo).AnyAsync();

        if (!hasPairRate)
        {
            errors.Add(
                new ValidationFailure("Rate", $"Pairs {request.SymbolFrom} <-> {request.SymbolTo} has no rates."));
        }

        return new ValidationResult(errors);
    }

    private static string ConstructMessage(CreatePaymentOrderCommand request,
        ExchangeRateDocument exchangeRate)
    {
        var sb = new StringBuilder();
        sb.AppendFormat(TelegramPaymentMessageTemplate.OrderId, request.OrderId)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientSymbolFrom, request.SymbolFrom)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientAmountFrom, request.AmountFrom)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientSymbolTo, request.SymbolTo)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientAmountTo, request.AmountTo)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ExchangeRate, exchangeRate.Price)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientWallet, request.ClientWallet)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.TransferWallet, request.TransferWallet)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.ClientEmail, request.ClientEmail)
            .AppendLine()
            .AppendFormat(TelegramPaymentMessageTemplate.OrderCreatedAt, request.CreatedAt);

        return sb.ToString();
    }
}