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

    public static class ShoppingCartSeedData
    {
        public static ShoppingCart[] GetSeedShoppingCarts()
        {
            return new ShoppingCart[]
            {
                new ShoppingCart
                {
                    CartId = Guid.Parse("4d5e6f7a-4567-8910-1234-56789abcdef1"),
                    UserId = Guid.Parse("b2c3d4e5-2345-6789-0123-bcdef1234567"), // John Doe
                    CreatedAt = new DateTime(2023, 4, 1)
                }
            };
        }
    }
}
