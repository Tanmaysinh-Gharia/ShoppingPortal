using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.SKU)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.RowVersion)
            .IsRowVersion();

            builder.HasIndex(p => p.SKU)
                .IsUnique();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(p => p.StockQuantity)
                .IsRequired();


            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");


            builder.HasOne(p => p.CreatedByUser)
                .WithMany(u => u.ProductsCreated)
                .HasForeignKey(p => p.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);


            
        }

    }
}
