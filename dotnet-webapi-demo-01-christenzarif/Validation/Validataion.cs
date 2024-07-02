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

    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult($"Maximum allowed file size is {_maxFileSize / (1024 * 1024)} MB.");
                }
            }

            return ValidationResult.Success!;
        }
    }

    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName).ToLower();
                if (!_extensions.Contains(extension))
                {
                    return new ValidationResult($"This '{extension}' extension is not allowed.");
                }
            }

            return ValidationResult.Success!;
        }
    }
}