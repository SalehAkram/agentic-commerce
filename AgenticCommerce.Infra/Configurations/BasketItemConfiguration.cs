using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgenticCommerce.Infra.Configurations;

public sealed class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.ToTable("BasketItems", table => table.HasCheckConstraint("CK_BasketItems_Quantity", "[Quantity] > 0"));
        builder.HasKey(item => item.Id);
        builder.HasIndex(item => new { item.BasketId, item.ProductId }).IsUnique();
        builder.HasOne<Product>().WithMany().HasForeignKey(item => item.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
