using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class StudentGradeResponseDto
    {
        public string Score { get; set; } = null!;
        public StudentResponseDto Student { get; set; } = new ();
    }
}
