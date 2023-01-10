namespace Exchange.Portal.Presentation.Common;

public class OffsetPagination : IOffsetPagination
{
    public int Offset { get; set; } = 0;
    
    public int Count { get; set; } = int.MaxValue;
}