using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Models.Entites;
using System;

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
                string code = requestDto.Code!;
                Dictionary<string, List<string>> errors = new()
                {
                    { "code", [code] }
                };
                throw new ConflictException(errors, $"Course with code {code} already exists");
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
                Dictionary<string, List<string>> errors = new ()
                {
                    { "id", [id.ToString()]  }
                };
                throw new NotFoundException(errors, $"Course with id {id} not found");
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
            Dictionary<string, List<string>> errors = [];
            // Check if a different course with the same Id exists?
            if (sameCodeCourse is not null && sameCodeCourse.Id != id)
            {
                string code = sameCodeCourse.Code!;
                errors.Add("code", [code]);
                throw new ConflictException(errors, $"Course with code {code} already exists");
            }

            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);

            if (course is null)
            {
                errors.Add("id", [id.ToString()]);
                throw new NotFoundException(errors, $"Course with id {id} not found");
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
                Dictionary<string, List<string>> errors = new()
                {
                    { "id", [id.ToString()] }
                };
                throw new NotFoundException(errors, $"Course with id {id} not found");
            }
            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<CourseStudentsResponseDto> EnrollStudent(long courseId, long studentId)
        {
            Dictionary<string, List<string>> errors = [];
            Course? course = await _dataContext.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == courseId);
            if (course is null)
            {
                errors.Add("id", [courseId.ToString()]);
                throw new NotFoundException(errors, $"Course with id {courseId} not found");
            }

            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student is null)
            {
                errors.Add("id", [studentId.ToString()]);
                throw new NotFoundException(errors, $"Student with id {studentId} not found");
            }

            course.Students.Add(student);
            await _dataContext.SaveChangesAsync();

            HashSet<StudentResponseDto> studentDtos = course.Students.Select(s => new StudentResponseDto
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                BirthDate = s.BirthDate
            }).ToHashSet();

            CourseStudentsResponseDto responseDto = new()
            {
                Id = course.Id,
                Code = course.Code,
                Name = course.Name,
                Description = course.Description,
                CourseStudents = studentDtos
            };

            return responseDto;
        }

        private async Task<Course?> getCourseByCode(string code)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Code == code);
            return course;
        }

    }
}
