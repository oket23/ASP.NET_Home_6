namespace Home_6.BLL.Wrappers;

public class GetAllWrapper<T>
{
    public IEnumerable<T> Items { get; set; }  = new List<T>();
    public int TotalCount { get; set; } 
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}