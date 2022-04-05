using CQRSApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CQRSApiTemplate.Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(t => t.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(t => t.Description)
            .HasMaxLength(100);

        builder.OwnsOne(t => t.Price)
            .Property(p => p.Amount)
            .HasColumnName("Price")
            .IsRequired()
            .HasColumnType("decimal(10,2)");

        builder.OwnsOne(t => t.Price)
            .Property(p => p.Currency)
            .HasColumnName("Currency")
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(10);
    }
}
