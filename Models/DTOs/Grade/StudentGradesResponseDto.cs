using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class StudentGradesResponseDto
    {
        public StudentResponseDto Student { get; set; } = new ();
        public HashSet<CourseGradeResponseDto> Grades { get; set; } = [];
    }
}
