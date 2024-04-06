using ShoppingPortal.Models.CustomValidation;
using System.ComponentModel.DataAnnotations;

namespace ShoppingPortal.Web.Models
{
    public class SignupViewModel
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [UniqueEmail(AllowUpdate = true)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "FirstName must be at least 1 characters long.")]
        public string FirstName { get; set; }
        [Required]
        [MinLength(1, ErrorMessage = "LastName must be at least 1 characters long.")]
        public string LastName { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }


        [RegularExpression(@"(^$|^\d{10}$)", ErrorMessage = "Mobile number must be either empty or exactly 10 digits.")]
        public string Mob { get; set; }

        [Required]
        [ValidCountry(ErrorMessage = "Invalid country selection")]
        public string Country { get; set; }

        [ValidState(ErrorMessage = "Invalid state for selected country")]
        public string? State { get; set; }

    }
}
