using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingPortal.Core.Enums;

namespace ShoppingPortal.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal UnitPrice { get; set; }


        [Required]
        [StringLength(20)]
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;
        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
