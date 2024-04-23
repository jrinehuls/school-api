using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs
{
    public class StudentRequestDto
    {
        // Allowing null is fine here because validation will
        // pick up on null, but be required, sending meaningful
        // response on fileds to correct
        [Required(AllowEmptyStrings = false)]
        public string? FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string? Address { get; set; }
    }
}
