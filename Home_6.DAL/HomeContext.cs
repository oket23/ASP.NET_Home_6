using Home_6.BLL.Models;
using Home_6.DLL.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Home_6.DLL;

public class HomeContext : DbContext
{
    public HomeContext(DbContextOptions<HomeContext> options) : base(options)
    {
    }
    
    public DbSet<Product> Products => Set<Product>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductsConfiguration).Assembly);
    }
}