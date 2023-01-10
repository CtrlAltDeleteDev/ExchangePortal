namespace Exchange.Portal.ApplicationCore.Models;

public record ExchangeRate(string SymbolFrom, string SymbolTo, decimal Amount);