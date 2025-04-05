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
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(20)]
        public OrderStatusEnum Status { get; set; } = OrderStatusEnum.Pending;

        [Required]
        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalAmount { get; set; }

        [Required]
        [ForeignKey("ShippingAddress")]
        public string ShippingPostalCode { get; set; }




        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<OrderStatusLog> StatusLogs { get; set; }
    }
}
