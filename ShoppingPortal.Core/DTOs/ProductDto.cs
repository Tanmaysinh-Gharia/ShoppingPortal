using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.DTOs
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SKU { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public List<CategoryDto> Categories { get; set; } = new();
        public string ImageUrl { get; set; }

        // Additional properties for cart functionality

        public int CurrentQuantity { get; set; } // Default quantity
        public bool IsInCart { get; set; } // To track if already in cart
    }
}
