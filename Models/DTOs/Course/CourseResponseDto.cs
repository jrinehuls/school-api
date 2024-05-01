
namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseResponseDto
    {
        public long Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
