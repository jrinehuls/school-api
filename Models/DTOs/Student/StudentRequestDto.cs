using SchoolAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs.Student
{
    public class StudentRequestDto
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [Required(AllowEmptyStrings = false)]
        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; } = null!;

        [Past]
        public DateTime? BirthDate { get; set; }
    }
}
