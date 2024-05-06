
namespace SchoolAPI.Models.DTOs.Student
{
    public class StudentResponseDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
