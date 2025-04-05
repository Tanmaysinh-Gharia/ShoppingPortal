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
    public static class ProductSeedData
    {
        public static Product[] GetSeedProducts()
        {
            return new Product[]
            {
                new Product
                {
                    ProductId = Guid.Parse("1a2b3c4d-1234-5678-9012-abcdef123456"),
                    Name = "Smartphone X",
                    Description = "Latest smartphone with advanced features",
                    SKU = "SPX-1001",
                    Price = 799.99m,
                    StockQuantity = 100,
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 3, 1)
                },
                new Product
                {
                    ProductId = Guid.Parse("2b3c4d5e-2345-6789-0123-bcdef1234567"),
                    Name = "Wireless Headphones",
                    Description = "Noise cancelling wireless headphones",
                    SKU = "WH-2002",
                    Price = 199.99m,
                    StockQuantity = 50,
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 3, 5)
                },
                new Product
                {
                    ProductId = Guid.Parse("3c4d5e6f-3456-7890-1234-cdef12345678"),
                    Name = "Cotton T-Shirt",
                    Description = "Premium quality cotton t-shirt",
                    SKU = "CT-3003",
                    Price = 24.99m,
                    StockQuantity = 200,
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 3, 10)
                }
            };
        }
    }
}
