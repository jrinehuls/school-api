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
        private readonly IEntityFinder<Course, long> _courseFinder;
        private readonly IEntityFinder<Student, long> _studentFinder;

        public CourseService(DataContext dataContext, IEntityFinder<Course, long> courseFinder, IEntityFinder<Student, long> studentFinder)
        {
            _dataContext = dataContext;
            _courseFinder = courseFinder;
            _studentFinder = studentFinder;
        }

        public async Task<CourseResponseDto> CreateCourse(CourseRequestDto requestDto)
        {
            if (await GetCourseByCode(requestDto.Code) is not null)
            {
                ThrowCourseCodeConflict(requestDto.Code);
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
            Course course = await _courseFinder.FindEntityByIdOrThrow(id);
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
            Course? sameCodeCourse = await GetCourseByCode(requestDto.Code);
            // Check if a different course with the same Id exists?
            if (sameCodeCourse is not null && sameCodeCourse.Id != id)
            {
                ThrowCourseCodeConflict(requestDto.Code);
            }

            Course course = await _courseFinder.FindEntityByIdOrThrow(id);

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
            Course course = await _courseFinder.FindEntityByIdOrThrow(id);
            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<CourseStudentsResponseDto> EnrollStudent(long courseId, long studentId)
        {
            Course course = await GetCourseByIdWithStudentsOrThrow(courseId);
            Student student = await _studentFinder.FindEntityByIdOrThrow(studentId);

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
                Students = studentDtos
            };

            return responseDto;
        }

        public async Task<CourseStudentsResponseDto> UnenrollStudent(long courseId, long studentId)
        {
            Course course = await GetCourseByIdWithStudentsOrThrow(courseId);
            Student student = await _studentFinder.FindEntityByIdOrThrow(studentId);

            // TODO: Check if course contains student and if not, throw StudentNotEnrolledException.
            course.Students.Remove(student);
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
                Students = studentDtos
            };

            return responseDto;
        }

        public async Task<List<StudentResponseDto>> GetEnrolledStudents(long id)
        {
            Course course = await GetCourseByIdWithStudentsOrThrow(id);
            List<StudentResponseDto> studentDtos = course.Students
                .Select(s => new StudentResponseDto
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    BirthDate = s.BirthDate
                }).ToList();
            return studentDtos;
        }

        // -------------------------------- Private Methods --------------------------------
        private async Task<Course?> GetCourseByCode(string code)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Code == code);
            return course;
        }

        private async Task<Course> GetCourseByIdWithStudentsOrThrow(long id)
        {
            Course? course = await _dataContext.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                ThrowNotFoundById(id);
            }
            return course!;
        }

        private static void ThrowNotFoundById(long id)
        {
            Dictionary<string, List<string>> errors = new()
                {
                    { "id", [$"{id}"] }
                };
            throw new NotFoundException(errors, $"Course with id {id} not found");
        }

        private static void ThrowCourseCodeConflict(string code)
        {
            Dictionary<string, List<string>> errors = new()
                {
                    { "code", [code] }
                };
            throw new ConflictException(errors, $"Course with code {code} already exists");
        }

    }
}
