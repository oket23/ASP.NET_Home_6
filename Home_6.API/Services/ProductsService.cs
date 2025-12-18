using Home_6.API.Interfaces;
using Home_6.API.Requests;
using Home_6.API.Responses;
using Home_6.BLL.Interfaces.Repositories;
using Home_6.BLL.Models;
using Home_6.BLL.Properties;
using Home_6.BLL.Wrappers;

namespace Home_6.API.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _repository;
    
    public ProductsService(IProductsRepository repository)
    {
        _repository = repository;
    }

   public async Task<GetAllWrapper<ProductResponse>> GetAllAsync(ProductProperties? properties)
    {
        var result = await _repository.GetAll(properties);

        return new GetAllWrapper<ProductResponse>
        {
            Items = result.Items.Select(MapToResponse),
            TotalCount = result.TotalCount,
            PageNumber = result.PageNumber,
            PageSize = result.PageSize
        };
    }

    public async Task<ProductResponse> GetByIdAsync(int id)
    {
        var product = await _repository.GetById(id);
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found");
        }

        return MapToResponse(product);
    }

    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var product = new Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            State = request.State,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            DeletedAt = null
        };

        var createdProduct = await _repository.Create(product);
        return MapToResponse(createdProduct);
    }

    public async Task<ProductResponse> UpdateAsync(int id, UpdateProductRequest request)
    {
        var product = await _repository.GetById(id);
        
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found for update");
        }
        
        product.Name = request.Name ?? product.Name;
        product.Description = request.Description ?? product.Description;
        product.Price = request.Price ?? product.Price;
        product.State = request.State ?? product.State;

        product.UpdatedAt = DateTime.UtcNow;

        await _repository.Update(product);
        return MapToResponse(product);
    }

    public async Task<ProductResponse> DeleteAsync(int id)
    {
        var deletedProduct = await _repository.Delete(id);
        
        if (deletedProduct == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found for deletion");
        }

        return MapToResponse(deletedProduct);
    }

    public async Task<ProductResponse> ChangeStateAsync(int id, ChangeStateProductRequest request)
    {
        var product = await _repository.ChangeState(id, request.State);
        
        if (product == null)
        {
            throw new KeyNotFoundException($"Product with id {id} not found for state change");
        }

        return MapToResponse(product);
    }


    private static ProductResponse MapToResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name, 
            Description = product.Description,
            Price = product.Price,
            State = product.State,
            CreatedAt = product.CreatedAt,
            UpdatedAt = product.UpdatedAt,
            DeletedAt = product.DeletedAt
        };
    }
}