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
            if (await GetCourseByCode(requestDto.Code) is not null)
            {
                throw new CourseConflictException("code", requestDto.Code);
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
            Course course = await FindByIdOrThrow(id);
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
            Course course = await FindByIdOrThrow(id);
            Course? courseWithSameCode = await GetCourseByCode(requestDto.Code);
            // If code exists and does not belong to same course, throw conflict
            if (courseWithSameCode is not null && courseWithSameCode.Id != course.Id)
            {
                throw new CourseConflictException("code", courseWithSameCode.Code);
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
            Course course = await FindByIdOrThrow(id);
            _dataContext.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<CourseStudentsResponseDto> EnrollStudent(long courseId, long studentId)
        {
            Course course = await FindByIdWithStudentsOrThrow(courseId);

            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student is null)
            {
                throw new StudentNotFoundException(studentId);
            }

            if (course.Students.Contains(student))
            {
                throw new StudentAlreadyEnrolledException(studentId, courseId);
            }

            course.Students.Add(student);
            await _dataContext.SaveChangesAsync();

            HashSet<StudentResponseDto> studentDtos = course.Students
                .Select(s => new StudentResponseDto
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
            Course course = await FindByIdWithStudentsOrThrow(courseId);

            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student is null)
            {
                throw new StudentNotFoundException(studentId);
            }

            if (!course.Students.Contains(student))
            {
                throw new StudentNotEnrolledException(studentId, courseId);
            }

            course.Students.Remove(student);
            await _dataContext.SaveChangesAsync();

            HashSet<StudentResponseDto> studentDtos = course.Students
                .Select(s => new StudentResponseDto
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
            Course course = await FindByIdWithStudentsOrThrow(id);
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

        private async Task<Course> FindByIdOrThrow(long id)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == id);
            if (course is null)
            {
                throw new CourseNotFoundException(id);
            }
            return course;
        }

        private async Task<Course> FindByIdWithStudentsOrThrow(long id)
        {
            Course? course = await _dataContext.Courses
                .Include(c => c.Students)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (course is null)
            {
                throw new CourseNotFoundException(id);
            }
            return course;
        }

    }
}
