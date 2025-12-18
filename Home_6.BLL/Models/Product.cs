using Home_6.BLL.Enums;

namespace Home_6.BLL.Models;

public class Product : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public ProductStateEnum State { get; set; }
}