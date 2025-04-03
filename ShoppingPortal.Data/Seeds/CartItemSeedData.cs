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

    public static class CartItemSeedData
    {
        public static CartItem[] GetSeedCartItems()
        {
            return new CartItem[]
            {
                new CartItem
                {
                    CartItemId = Guid.Parse("5e6f7a8b-5678-9101-2345-6789abcdef12"),
                    CartId = Guid.Parse("4d5e6f7a-4567-8910-1234-56789abcdef1"), // John's cart
                    ProductId = Guid.Parse("1a2b3c4d-1234-5678-9012-abcdef123456"), // Smartphone X
                    Quantity = 1,
                    AddedAt = new DateTime(2023, 4, 2)
                },
                new CartItem
                {
                    CartItemId = Guid.Parse("6f7a8b9c-6789-1011-3456-789abcdef123"),
                    CartId = Guid.Parse("4d5e6f7a-4567-8910-1234-56789abcdef1"), // John's cart
                    ProductId = Guid.Parse("3c4d5e6f-3456-7890-1234-cdef12345678"), // Cotton T-Shirt
                    Quantity = 2,
                    AddedAt = new DateTime(2023, 4, 3)
                }
            };
        }
    }
}
