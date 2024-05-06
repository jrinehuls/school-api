using AutoMapper;
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
        private readonly IMapper _mapper;

        public CourseService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<CourseResponseDto> CreateCourse(CourseRequestDto requestDto)
        {
            if (await GetCourseByCode(requestDto.Code) is not null)
            {
                throw new CourseConflictException("code", requestDto.Code);
            }

            Course course = _mapper.Map<Course>(requestDto)!;

            _dataContext.Add(course);
            await _dataContext.SaveChangesAsync();

            CourseResponseDto responseDto = _mapper.Map<CourseResponseDto>(course)!;

            return responseDto;
        }

        public async Task<List<CourseResponseDto>> GetAllCourses()
        {
            List<CourseResponseDto> responseDtos = await _dataContext.Courses
                .Select(c => _mapper.Map<CourseResponseDto>(c)!)
                .ToListAsync();
            return responseDtos;
        }

        public async Task<CourseResponseDto> GetCourseById(long id)
        {
            Course course = await FindByIdOrThrow(id);
            CourseResponseDto responseDto = _mapper.Map<CourseResponseDto>(course)!;

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

            CourseResponseDto responseDto = _mapper.Map<CourseResponseDto>(course)!;

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

            CourseStudentsResponseDto responseDto = _mapper.Map<CourseStudentsResponseDto>(course)!;

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

            CourseStudentsResponseDto responseDto = _mapper.Map<CourseStudentsResponseDto>(course)!;

            return responseDto;
        }

        public async Task<List<StudentResponseDto>> GetEnrolledStudents(long id)
        {
            Course course = await FindByIdWithStudentsOrThrow(id);
            List<StudentResponseDto> studentDtos = course.Students
                .Select(s => _mapper.Map<StudentResponseDto>(s)!).ToList();
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
