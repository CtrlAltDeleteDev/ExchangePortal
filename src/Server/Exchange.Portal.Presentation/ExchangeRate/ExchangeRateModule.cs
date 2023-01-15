using System.ComponentModel.DataAnnotations;
using Exchange.Portal.ApplicationCore.Features.ExchangeRate.Queries;

namespace Exchange.Portal.Presentation.ExchangeRate;

internal sealed class ExchangeRateModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/exchange-rate/{symbolFrom}/{symbolTo}", async (
            [Required(AllowEmptyStrings = false)] string symbolFrom,
            [Required(AllowEmptyStrings = false)] string symbolTo,
            ISender sender) =>
        {
            var exchangeRate =
                await sender.Send(new FetchExchangeRateQuery(symbolFrom, symbolTo));

            return Results.Ok(exchangeRate);
        });
    }
}