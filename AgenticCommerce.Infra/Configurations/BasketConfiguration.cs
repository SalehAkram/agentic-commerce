using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgenticCommerce.Infra.Configurations;

public sealed class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.ToTable("Baskets");
        builder.HasKey(basket => basket.Id);
        builder.Property(basket => basket.Status).HasConversion<string>().HasMaxLength(20).IsRequired();
        builder.Property(basket => basket.UpdatedAtUtc).IsRequired();
        builder.HasOne<Customer>().WithMany().HasForeignKey(basket => basket.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(basket => basket.CustomerId).IsUnique().HasFilter("[Status] = 'Active'");
        builder.HasMany(basket => basket.Items).WithOne().HasForeignKey(item => item.BasketId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(basket => basket.Items).HasField("_items")
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
