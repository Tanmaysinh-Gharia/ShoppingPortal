using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoppingPortal.Data.Entities;
namespace ShoppingPortal.Data.Configurations
{
    public class OrderStatusLogConfiguration : IEntityTypeConfiguration<OrderStatusLog>
    {
        public void Configure(EntityTypeBuilder<OrderStatusLog> builder)
        {
            builder.HasKey(osl => osl.LogId);

            builder.Property(osl => osl.OldStatus)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(osl => osl.NewStatus)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(osl => osl.ChangedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(osl => osl.Order)
                .WithMany(o => o.StatusLogs)
                .HasForeignKey(osl => osl.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(osl => osl.Product)
                .WithMany(p => p.StatusLogs)
                .HasForeignKey(osl => osl.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(osl => osl.ChangedByUser)
                .WithMany(u => u.StatusChangesMade)
                .HasForeignKey(osl => osl.ChangedBy)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
