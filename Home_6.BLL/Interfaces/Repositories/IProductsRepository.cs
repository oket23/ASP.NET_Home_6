using Home_6.BLL.Enums;
using Home_6.BLL.Models;
using Home_6.BLL.Properties;
using Home_6.BLL.Wrappers;

namespace Home_6.BLL.Interfaces.Repositories;

public interface IProductsRepository
{
    Task<GetAllWrapper<Product>> GetAll(ProductProperties? properties);
    Task<Product?> GetById(int id);
    Task<Product> Create(Product product);
    Task<Product?> Update(Product product);
    Task<Product?> ChangeState(int id, ProductStateEnum stateEnum);
    Task<Product?> Delete(int id);
}
