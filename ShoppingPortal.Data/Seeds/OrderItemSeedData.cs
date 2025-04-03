using ShoppingPortal.Core.Helpers;
using ShoppingPortal.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Seeds
{

    public static class OrderItemSeedData
    {
        public static OrderItem[] GetSeedOrderItems()
        {
            return new OrderItem[]
            {
                new OrderItem
                {
                    OrderItemId = Guid.Parse("8b9c0d1e-8901-1213-4567-89abcdef1234"),
                    OrderId = Guid.Parse("7a8b9c0d-7890-1112-3456-789abcdef123"), // John's order
                    ProductId = Guid.Parse("1a2b3c4d-1234-5678-9012-abcdef123456"), // Smartphone X
                    Quantity = 1,
                    UnitPrice = 799.99m
                },
                new OrderItem
                {
                    OrderItemId = Guid.Parse("9c0d1e2f-9012-1314-5678-9abcdef12345"),
                    OrderId = Guid.Parse("7a8b9c0d-7890-1112-3456-789abcdef123"), // John's order
                    ProductId = Guid.Parse("3c4d5e6f-3456-7890-1234-cdef12345678"), // Cotton T-Shirt
                    Quantity = 2,
                    UnitPrice = 24.99m
                }
            };
        }
    }
}
