using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Models.DTOs.Grade
{
    public class CourseGradeResponseDto
    {
        public string Score { get; set; } = null!;
        public CourseResponseDto Course { get; set; } = new ();
    }
}
