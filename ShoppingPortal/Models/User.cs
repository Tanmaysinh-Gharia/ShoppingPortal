using ShoppingPortal.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace ShoppingPortal.Web.Models
{

    public class User
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string FirstName { get; set; }

        public string Role { get; set; } = "User";

        public string? Password { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name must be between 2 and 50 characters.", MinimumLength = 2)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [UniqueEmail(AllowUpdate = true)]
        public string Email { get; set; }

        [RegularExpression(@"(^$|^\d{10}$)", ErrorMessage = "Mobile number must be either empty or exactly 10 digits.")]
        public string? Mob { get; set; }

        [Required(ErrorMessage = "State is required.")]
        [ValidState(ErrorMessage = "Invalid state for selected country")]
        public string State { get; set; }
        
        [Required(ErrorMessage = "City is required.")]
        [ValidState(ErrorMessage = "Invalid City for selected country")]
        public string City { get; set; }
        
        
        [Required(ErrorMessage = "PinCode is required.")]
        [ValidState(ErrorMessage = "Invalid Pincode for selected country")]
        public string PostalCode { get; set; }

    }
}
