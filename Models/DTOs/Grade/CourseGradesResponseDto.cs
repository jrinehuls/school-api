using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class CourseGradesResponseDto
    {
        public CourseResponseDto Course { get; set; } = new();
        public HashSet<StudentGradeResponseDto> Grades { get; set; } = [];
    }
}
