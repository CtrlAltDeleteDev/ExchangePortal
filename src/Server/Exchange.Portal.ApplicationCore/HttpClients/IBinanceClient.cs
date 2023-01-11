using Refit;

namespace Exchange.Portal.ApplicationCore.HttpClients;

public interface IBinanceClient
{
    [Get("/api/v3/ticker/price?symbol={symbolFrom}{symbolTo}")]
    Task<ApiResponse<RateResponse>> GetRateAsync(string symbolFrom, string symbolTo);
}