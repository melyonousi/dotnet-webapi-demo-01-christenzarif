using System.ComponentModel.DataAnnotations;

namespace dotnet_webapi_demo_01_christenzarif.Validation
{
    public class ValidateAgeAttribute : ValidationAttribute
    {
        public ValidateAgeAttribute()
        {
            string defaultErrorMessage = $"Birthdate must be lower than {DateTime.Today.ToString("yyyy-MM-dd")}.";
            ErrorMessage ??= defaultErrorMessage;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Birthdate is Required", new[] {nameof(validationContext.DisplayName) });
            }

            if (Convert.ToDateTime(value).Date >= DateTime.Today.AddYears(-1))
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }
    }
}