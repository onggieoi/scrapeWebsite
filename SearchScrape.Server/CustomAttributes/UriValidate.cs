using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class UrlValidationAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        // Check if the value is null or empty (allowing for optional URLs)
        if (value == null || string.IsNullOrEmpty(value.ToString()))
        {
            return ValidationResult.Success;
        }

        // Regular expression to validate URLs
        var urlPattern = @"^https?:\/\/[^\s$.?#].[^\s]*$";
        var regex = new Regex(urlPattern, RegexOptions.IgnoreCase);

        if (regex.IsMatch(value.ToString()))
        {
            return ValidationResult.Success;
        }
        else
        {
            return new ValidationResult("The URL is not valid.");
        }
    }
}
