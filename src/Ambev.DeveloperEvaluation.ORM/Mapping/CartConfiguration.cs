using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.UserId).IsRequired();
        builder.Property(c => c.Date).IsRequired();
        builder.Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");
        builder.Property(c => c.IsCancelled).IsRequired();

        builder.OwnsMany(c => c.Items, i =>
        {
            i.WithOwner().HasForeignKey("CartId");
            i.Property<Guid>("Id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            i.HasKey("Id");
            i.ToTable("CartItems");
            i.Property(ci => ci.ProductId).IsRequired();
            i.Property(ci => ci.Quantity).IsRequired();
            i.Property(ci => ci.UnitPrice).HasColumnType("decimal(18,2)");
            i.Property(ci => ci.Discount).HasColumnType("decimal(18,2)");
            i.Property(ci => ci.TotalAmount).HasColumnType("decimal(18,2)");
        });
    }
}