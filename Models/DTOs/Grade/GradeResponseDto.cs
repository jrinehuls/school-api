using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class GradeResponseDto
    {
        public long Id { get; set; }
        public string? Score { get; set; }
        public StudentResponseDto? Student { get; set; }
        public CourseResponseDto? Course { get; set; }
    }
}
