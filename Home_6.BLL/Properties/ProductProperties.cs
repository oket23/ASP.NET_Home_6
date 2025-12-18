using Home_6.BLL.Enums;

namespace Home_6.BLL.Properties;

public class ProductProperties : PaginationBase
{
    public string? Name { get; set; }
    public ProductStateEnum? State { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public DateTime? CreatedFrom { get; set; }
    public DateTime? CreatedTo { get; set; }
    public bool IncludeDeleted { get; set; } = false;
    public ProductOrderByEnum OrderBy { get; set; } = ProductOrderByEnum.CreatedAt;
}