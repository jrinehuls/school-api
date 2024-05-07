using SchoolAPI.Models.DTOs.Grade;

namespace SchoolAPI.Models.DTOs.Course
{
    public class CourseGradeResponseDto
    {
        public long Id {  get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public GradeScoreResponseDto? Grade { get; set; }
    }
}
