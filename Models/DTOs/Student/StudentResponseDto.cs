using System.ComponentModel.DataAnnotations;

namespace SchoolAPI.Models.DTOs.Student
{
    public class StudentResponseDto
    {
        public long Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
    }
}
