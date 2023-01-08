using System.Text;
using Exchange.Portal.ApplicationCore.Configurations;
using Exchange.Portal.ApplicationCore.Consts;
using Exchange.Portal.ApplicationCore.Extensions;
using Exchange.Portal.Infrastructure.Documents;
using Marten;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Exchange.Portal.ApplicationCore.Features.Payment.Command;

public static class CreatePaymentCommand
{
    public record Payment(string Id, string SymbolFrom, decimal AmountFrom, string SymbolTo, decimal AmountTo,
        DateTimeOffset CreatedAt, string TransferWallet, string ClientEmail, string ClientWallet) : IRequest<Unit>;
    
    public class Handler : IRequestHandler<Payment, Unit>
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly TelegramBotSettings _botSettings;
        private readonly IDocumentStore _documentStore;
        private readonly ILogger<Handler> _logger;

        public Handler(ITelegramBotClient telegramBotClient, TelegramBotSettings botSettings, IDocumentStore documentStore, ILogger<Handler> logger)
        {
            _telegramBotClient = telegramBotClient;
            _botSettings = botSettings;
            _documentStore = documentStore;
            _logger = logger;
        }

        public async Task<Unit> Handle(Payment request, CancellationToken cancellationToken)
        {
            await using IDocumentSession session = _documentStore.LightweightSession();
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

        private static string ConstructMessage(Payment request, ExchangeRateDocument exchangeRate)
        {
            var sb = new StringBuilder();
            sb.AppendFormat(TelegramPaymentMessageTemplate.OrderId, request.Id)
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
}