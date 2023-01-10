namespace Exchange.Portal.Infrastructure.Documents;

public class ReviewDocument
{
    public string Id { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string UserEmail { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;
    
    public DateTimeOffset CreatedAt { get; set; }
    
    public bool IsReviewedByAdmin { get; set; }
}