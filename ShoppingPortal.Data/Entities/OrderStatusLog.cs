using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Entities
{
    public class OrderStatusLog
    {
        [Key]
        public Guid LogId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public Guid OrderId { get; set; }

        [ForeignKey("Product")]
        public Guid? ProductId { get; set; }

        [Required]
        [StringLength(20)]
        public string OldStatus { get; set; }

        [Required]
        [StringLength(20)]
        public string NewStatus { get; set; }

        [Required]
        [ForeignKey("ChangedByUser")]
        public Guid ChangedBy { get; set; }

        [Required]
        public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Order Order { get; set; }
        public Product Product { get; set; }
        public User ChangedByUser { get; set; }
    }
}
