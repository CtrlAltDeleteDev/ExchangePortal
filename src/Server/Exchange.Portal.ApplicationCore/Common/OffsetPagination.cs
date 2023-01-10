namespace Exchange.Portal.ApplicationCore.Common;

public interface IOffsetPagination
{
    public int Offset { get; set; }
    
    public int Count { get; set; }
}