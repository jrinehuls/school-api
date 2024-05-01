using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Services
{
    public interface ICourseService
    {
        public Task<List<CourseResponseDto>> GetAllCourses();
        public Task<CourseResponseDto> GetCourseById(long id);
        public Task<CourseResponseDto> CreateCourse(CourseRequestDto requestDto);
        public Task<CourseResponseDto> UpdateCourse(long id, CourseRequestDto requestDto);
        public Task DeleteCourse(long id);
    }
}
