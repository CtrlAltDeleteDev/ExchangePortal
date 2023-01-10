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
            ApplicationCore.Models.ExchangeRate exchangeRate =
                await sender.Send(new ExchangeRateQuery(symbolFrom, symbolTo));

            return Results.Ok(exchangeRate);
        });
    }
}