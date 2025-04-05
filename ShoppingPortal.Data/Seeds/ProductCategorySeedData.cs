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
    public static class ProductCategorySeedData
    {
        public static ProductCategory[] GetSeedProductCategories()
        {
            return new ProductCategory[]
            {
                new ProductCategory
                {
                    ProductId = Guid.Parse("1a2b3c4d-1234-5678-9012-abcdef123456"),

                    CategoryId = Guid.Parse("d4e5f6a7-4567-8910-1234-56789abcdef1"), // Electronics
                    
                },
                new ProductCategory
                {
                    ProductId = Guid.Parse("2b3c4d5e-2345-6789-0123-bcdef1234567"),
                    
                    CategoryId = Guid.Parse("d4e5f6a7-4567-8910-1234-56789abcdef1"), // Electronics
                    
                },
                new ProductCategory
                {
                    ProductId = Guid.Parse("3c4d5e6f-3456-7890-1234-cdef12345678"),
                    
                    CategoryId = Guid.Parse("e5f6a7b8-5678-9101-2345-6789abcdef12"), // Clothing
                    
                }
            };
        }
    }
}
