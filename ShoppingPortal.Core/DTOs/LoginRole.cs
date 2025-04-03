using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingPortal.Core.DTOs
{
    public class LoginRole
    {
        public string Username { get; set; } // Composite of FirstName + LastName

        public string PasswordHash { get; set; }

        public bool IsActive { get; set; }
        public string Role { get; set; }     // UserType from User entity
        public string Email { get; set; }
        public Guid UserId { get; set; }
    }
}
