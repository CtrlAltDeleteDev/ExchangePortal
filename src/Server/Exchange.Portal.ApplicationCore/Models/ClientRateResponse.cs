namespace Exchange.Portal.ApplicationCore.Models;

public class ClientRateResponse
{
    public string Symbol { get; set; } = string.Empty;
    
    public decimal Price { get; set; }
}