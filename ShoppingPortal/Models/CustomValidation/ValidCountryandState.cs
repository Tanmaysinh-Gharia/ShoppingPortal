using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
using ShoppingPortal.Web.Models;
using ShoppingPortal.Services.UserServices;

public class ValidCountryAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var countryService = validationContext.GetService<CountryService>();
        var validCountries = countryService.GetCountryStates().Keys.ToList();

        if (value is string country && validCountries.Contains(country))
        {
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid country selection");
    }
}

public class ValidStateAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var countryService = validationContext.GetService<CountryService>();

        try
        {
            SignupViewModel user = (SignupViewModel)validationContext.ObjectInstance;
            if (string.IsNullOrEmpty(user.Country) || value == null)
                return ValidationResult.Success;

            List<String> states = countryService.GetCountryStates()
                .GetValueOrDefault(user.Country, new List<string>());

            return states.Contains(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult("Invalid state for selected country");
        }
        catch
        {
            SignupViewModel user = (SignupViewModel)validationContext.ObjectInstance;
            // Skip validation if no country selected or state is empty
            if (string.IsNullOrEmpty(user.Country) || value == null)
                return ValidationResult.Success;

            List<String> states = countryService.GetCountryStates()
                .GetValueOrDefault(user.Country, new List<string>());

            return states.Contains(value.ToString())
                ? ValidationResult.Success
                : new ValidationResult("Invalid state for selected country");
        }


    }
}