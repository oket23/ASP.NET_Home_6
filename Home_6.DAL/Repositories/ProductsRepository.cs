using Home_6.BLL.Enums;
using Home_6.BLL.Interfaces.Repositories;
using Home_6.BLL.Models;
using Home_6.BLL.Properties;
using Home_6.BLL.Wrappers;
using Microsoft.EntityFrameworkCore;

namespace Home_6.DLL.Repositories;

public class ProductsRepository : BaseRepository<Product>, IProductsRepository
{
    public ProductsRepository(HomeContext context) : base(context)
    {
    }

   public async Task<GetAllWrapper<Product>> GetAll(ProductProperties? properties)
    {
        properties ??= new ProductProperties();

        var query = Set.AsQueryable();
        
        if (properties.IncludeDeleted)
        {
            query = query.IgnoreQueryFilters();
        }
        
        if (!string.IsNullOrWhiteSpace(properties.Name))
        {
            query = query.Where(p => p.Name.Contains(properties.Name));
        }
        
        if (properties.State.HasValue)
        {
            query = query.Where(p => p.State == properties.State.Value);
        }
        
        if (properties.MinPrice.HasValue)
        {
            query = query.Where(p => p.Price >= properties.MinPrice.Value);
        }

        if (properties.MaxPrice.HasValue)
        {
            query = query.Where(p => p.Price <= properties.MaxPrice.Value);
        }
        
        if (properties.CreatedFrom.HasValue)
        {
            query = query.Where(p => p.CreatedAt >= properties.CreatedFrom.Value);
        }

        if (properties.CreatedTo.HasValue)
        {
            query = query.Where(p => p.CreatedAt <= properties.CreatedTo.Value);
        }
        
        query = properties.OrderBy switch
        {
            ProductOrderByEnum.Name => query.OrderBy(p => p.Name),
            ProductOrderByEnum.Price => query.OrderBy(p => p.Price),
            ProductOrderByEnum.UpdatedAt => query.OrderBy(p => p.UpdatedAt),
            _ => query.OrderBy(p => p.CreatedAt) 
        };
        
        var totalCount = await query.CountAsync();
        
        var items = await query
            .Skip((properties.PageNumber - 1) * properties.PageSize)
            .Take(properties.PageSize)
            .ToListAsync();

        return new GetAllWrapper<Product>
        {
            Items = items,
            TotalCount = totalCount,
            PageNumber = properties.PageNumber,
            PageSize = properties.PageSize
        };
    }

    public async Task<Product?> ChangeState(int id, ProductStateEnum stateEnum)
    {
        var product = await GetById(id);
        if (product == null)
        {
            return null;
        }

        product.State = stateEnum;
        await Context.SaveChangesAsync();
        return product;
    }
}