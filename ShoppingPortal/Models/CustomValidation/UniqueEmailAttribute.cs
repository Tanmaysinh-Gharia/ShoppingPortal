using System.ComponentModel.DataAnnotations;
using ShoppingPortal.Data;
using ShoppingPortal.Data.Interfaces;

namespace ShoppingPortal.Models.CustomValidation
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        public bool AllowUpdate { get; set; } = false;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbContext = validationContext.GetService<IUserRepository>();
            string email = value?.ToString();

            // Skip validation if the email is not yet assigned or if it's an update action
            var controller = (Microsoft.AspNetCore.Mvc.ControllerBase)validationContext.GetService(typeof(Microsoft.AspNetCore.Mvc.ControllerBase));
            var isUpdateAction = controller?.RouteData.Values["action"]?.ToString() == "UpdateProfile";
            
            if (AllowUpdate && isUpdateAction)
            {
                if (email == null)
                    return ValidationResult.Success;
                if (dbContext.GetByEmailAsync(email) != null)
                    return new ValidationResult("Email is already taken.");
            }

            return ValidationResult.Success;
        }
    }
}