using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Entities
{
    public class CartItem
    {
        [Key]
        public Guid CartItemId { get; set; }

        [Required]
        [ForeignKey("ShoppingCart")]
        public Guid CartId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public DateTime AddedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public ShoppingCart ShoppingCart { get; set; }
        public Product Product { get; set; }
    }
}
