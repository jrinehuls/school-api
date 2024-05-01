
namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseResponseDto
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
