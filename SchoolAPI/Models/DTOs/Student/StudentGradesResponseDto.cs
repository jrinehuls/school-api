using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Models.DTOs.Student
{
    public class StudentGradesResponseDto
    {
        public long Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public HashSet<CourseGradeResponseDto> CourseGrades { get; set; } = [];
    }
}
