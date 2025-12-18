using Home_6.API.Requests;
using Home_6.API.Responses;
using Home_6.BLL.Properties;
using Home_6.BLL.Wrappers;

namespace Home_6.API.Interfaces;

public interface IProductsService
{
    Task<GetAllWrapper<ProductResponse>> GetAllAsync(ProductProperties? properties);
    Task<ProductResponse> GetByIdAsync(int id);
    Task<ProductResponse> CreateAsync(CreateProductRequest request);
    Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request); 
    Task<ProductResponse> DeleteAsync(int id);
    Task<ProductResponse> ChangeStateAsync(int id, ChangeStateProductRequest request); 
}