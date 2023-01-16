using System.ComponentModel.DataAnnotations;
using Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

namespace Exchange.Portal.Presentation.ExchangeRate;

public sealed class ExchangeRateModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/exchange-rate/{symbolFrom}/{symbolTo}", async (
            [Required(AllowEmptyStrings = false)] string symbolFrom,
            [Required(AllowEmptyStrings = false)] string symbolTo,
            ISender sender) =>
        {
            Result<ApplicationCore.Models.ExchangeRate> exchangeRateResult =
                await sender.Send(new FetchExchangeRateQuery(symbolFrom, symbolTo));

            return exchangeRateResult.ToOk();
        });
    }
}