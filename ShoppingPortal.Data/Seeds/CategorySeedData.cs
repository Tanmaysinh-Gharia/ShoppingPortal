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
    public static class CategorySeedData
    {
        public static Category[] GetSeedCategories()
        {
            return new Category[]
            {
                new Category
                {
                    CategoryId = Guid.Parse("d4e5f6a7-4567-8910-1234-56789abcdef1"),
                    Name = "Electronics",
                    Description = "Electronic devices and accessories",
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 2, 1)
                },
                new Category
                {
                    CategoryId = Guid.Parse("e5f6a7b8-5678-9101-2345-6789abcdef12"),
                    Name = "Clothing",
                    Description = "Men's and women's clothing",
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 2, 5)
                },
                new Category
                {
                    CategoryId = Guid.Parse("f6a7b8c9-6789-1011-3456-789abcdef123"),
                    Name = "Home & Kitchen",
                    Description = "Home appliances and kitchenware",
                    CreatedBy = Guid.Parse("a1b2c3d4-1234-5678-9012-abcdef123456"), // Admin
                    CreatedAt = new DateTime(2023, 2, 10)
                }
            };
        }
    }
}
