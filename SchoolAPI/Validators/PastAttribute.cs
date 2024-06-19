using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Validators
{
    public class PastAttribute : ValidationAttribute
    {
        private string DefaulErrorMessage { get; set; } = "Date should be in the past.";
        
        public PastAttribute() {
        }
        
        // Does not force date to be required, but if it is, it must be valid
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                DateTime date = (DateTime)value;
                if (date >= DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? DefaulErrorMessage);
                }
                return ValidationResult.Success;
            }
            return null;
        }
    }
}
