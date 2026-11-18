using AgenticCommerce.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgenticCommerce.Infra.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.Id).ValueGeneratedNever();
        builder.Property(customer => customer.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.LastName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.Email).IsRequired().HasMaxLength(254);
        builder.HasIndex(customer => customer.Email).IsUnique();
    }
}
