using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseGradesResponseDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public HashSet<StudentGradeResponseDto> StudentGrades = []; 
    }
}
