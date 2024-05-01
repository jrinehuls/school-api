using SchoolAPI.Data;
using SchoolAPI.Models.DTOs.Course;

namespace SchoolAPI.Services.Impl
{
    public class CourseService : ICourseService
    {
        private readonly DataContext _dataContext;

        public CourseService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<CourseResponseDto> CreateCourse(CourseRequestDto requestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseResponseDto>> GetAllCourses()
        {
            throw new NotImplementedException();
        }

        public async Task<CourseResponseDto> GetCourseById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseResponseDto> UpdateCourse(long id, CourseRequestDto requestDto)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteCourse(long id)
        {
            throw new NotImplementedException();
        }
    }
}
