using Home_6.BLL.Enums;

namespace Home_6.API.Requests;

public class ChangeStateProductRequest
{
    public ProductStateEnum State { get; set; }
}