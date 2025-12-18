using Home_6.BLL.Enums;

namespace Home_6.API.Requests;

public class CreateProductRequest
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }
    public ProductStateEnum State { get; set; }
}