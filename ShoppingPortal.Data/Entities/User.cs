using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Data.Entities
{
    public class User
    {

        #region Scalar Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(256)]
        public string PasswordHash { get; set; }

        [StringLength(20)]
        public string PhoneNumber { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        [StringLength(20)]
        public string UserType { get; set; } // 'Customer', 'Admin', 'Vendor'

        [StringLength(200)]
        public string StreetAddress { get; set; }

        [ForeignKey("Address")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        #endregion

        #region Navigation properties ( Lazy Loading ) 

        public virtual Address Address { get; set; }
        public virtual ICollection<Category> CategoriesCreated { get; set; }
        public virtual ICollection<Product> ProductsCreated { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<OrderStatusLog> StatusChangesMade { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }

        #endregion
        
    }
}
