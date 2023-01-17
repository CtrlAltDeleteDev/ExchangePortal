namespace Exchange.Portal.ApplicationCore.Models;

public class Review
{
    public string UserName { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
}