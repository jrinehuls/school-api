using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseStudentsResponseDto : CourseResponseDto
    {
        public HashSet<StudentResponseDto> CourseStudents { get; set; } = [];
    }
}
