using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.Entites;

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
            if (await getCourseByCode(requestDto.Code!) is not null)
            {
                throw new ConflictException(nameof(requestDto.Code), requestDto.Code!);
            }

            Course course = new()
            {
                Code = requestDto.Code,
                Name = requestDto.Name,
                Description = requestDto.Description
            };

            _dataContext.Add(course);
            await _dataContext.SaveChangesAsync();

            CourseResponseDto responseDto = new()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                Description = course.Description,
            };

            return responseDto;
        }

        public async Task<List<CourseResponseDto>> GetAllCourses()
        {
            List<CourseResponseDto> responseDtos = await _dataContext.Courses
                .Select(c => new CourseResponseDto()
                    {
                        Id = c.Id,
                        Code = c.Code,
                        Name = c.Name,
                        Description = c.Description
                    })
                .ToListAsync();
            return responseDtos;
        }

        public async Task<CourseResponseDto> GetCourseById(long id)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                throw new NotFoundException(nameof(Course.Id), id.ToString());
            }

            CourseResponseDto responseDto = new ()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                Description = course.Description,
            };

            return responseDto;
        }

        public async Task<CourseResponseDto> UpdateCourse(long id, CourseRequestDto requestDto)
        {
            Course? sameCodeCourse = await getCourseByCode(requestDto.Code!);
            // Check if a different course with the same Id exists?
            if (sameCodeCourse is not null && sameCodeCourse.Id != id)
            {
                throw new ConflictException(nameof(Course.Code), sameCodeCourse.Code!);
            }

            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course is null)
            {
                throw new NotFoundException(nameof(Course.Id), id.ToString());
            }

            course.Code = requestDto.Code;
            course.Name = requestDto.Name;
            course.Description = requestDto.Description;

            await _dataContext.SaveChangesAsync();

            CourseResponseDto responseDto = new ()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                Description = course.Description
            };

            return responseDto;
        }

        public async Task DeleteCourse(long id)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                throw new NotFoundException(nameof(Course.Id), id.ToString());
            }
            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();
        }

        private async Task<Course?> getCourseByCode(string code)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Code == code);
            return course;
        }
    }
}
