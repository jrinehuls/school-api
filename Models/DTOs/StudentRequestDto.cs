using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs
{
    public class StudentRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        public required string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string LastName { get; set; }

        [EmailAddress]
        public required string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        public required string Address { get; set; }
    }
}
