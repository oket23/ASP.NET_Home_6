using Home_6.BLL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Home_6.DLL.Configurations;

public class ProductsConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        
        builder.Property(p => p.UpdatedAt)
            .IsRequired();

        builder.Property(p => p.DeletedAt)
            .IsRequired(false);
        
        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(p => p.Price)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(p => p.State)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(50);
        
        builder.HasIndex(p => p.Name);
        builder.HasIndex(p => p.State);
        builder.HasIndex(p => p.Price);
        
        builder.HasQueryFilter(p => p.DeletedAt == null);
    }
}