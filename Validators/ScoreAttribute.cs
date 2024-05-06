using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Validators
{
    public class ScoreAttribute : ValidationAttribute
    {
        private static readonly string[] scores = [ "A+", "A", "A-",
                                                    "B+", "B", "B-",
                                                    "C+", "C", "C-",
                                                    "D+", "D", "D-",
                                                    "F" ];

        public ScoreAttribute() {
            ErrorMessage = SetDefaultErrorMessage();
        }
        public ScoreAttribute(string message)
        {
            ErrorMessage = message;
        }

        // Does not force score to be required, but if it is, it must be valid
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not null)
            {
                string score = (string)value;
                if (!scores.Contains(score.ToUpper()))
                {
                    return new ValidationResult(ErrorMessage);
                }
                return ValidationResult.Success;
            }
            return null;
        }

        private string SetDefaultErrorMessage()
        {
            string message = "The value must be one of the following: ";
            for (int i = 0; i < scores.Length - 1; i++)
            {
                message += scores[i] + ", ";
            }
            message += scores[scores.Length];

            return message;
        }
    }
}
