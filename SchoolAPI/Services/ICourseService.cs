using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Services
{
    public interface ICourseService
    {
        public Task<List<CourseResponseDto>> GetAllCourses();
        public Task<CourseResponseDto> GetCourseById(long id);
        public Task<CourseResponseDto> CreateCourse(CourseRequestDto requestDto);
        public Task<CourseResponseDto> UpdateCourse(long id, CourseRequestDto requestDto);
        public Task DeleteCourse(long id);
        public Task<CourseStudentsResponseDto> EnrollStudent(long courseId, long studentId);
        public Task<CourseStudentsResponseDto> UnenrollStudent(long courseId, long studentId);
        public Task<List<StudentResponseDto>> GetEnrolledStudents(long id);
    }
}
