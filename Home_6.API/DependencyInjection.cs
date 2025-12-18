using Home_6.API.Interfaces;
using Home_6.API.Services;
using Home_6.BLL.Interfaces.Repositories;
using Home_6.DLL;
using Home_6.DLL.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Home_6.API;

public static class DependencyInjection
{
    public static IServiceCollection AddWebApi(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<HomeContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<IProductsService, ProductsService>();
        
        return services;
    }
}