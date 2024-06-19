using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Models.DTOs.Student
{
    public class StudentCoursesResponseDto : StudentResponseDto
    {
        public HashSet<CourseResponseDto> Courses { get; set; } = [];
    }
}
