using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs.Grade;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class GradeService : IGradeService
    {

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public GradeService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<GradeResponseDto> CreateGrade(GradeRequestDto requestDto, long studentId, long courseId)
        {
            Student student = await FindStudnetByIdOrThrow(studentId);
            Course course = await FindCourseByIdOrThrow(courseId);
            Grade? existingGrade = await _dataContext.Grades
                .FirstOrDefaultAsync(g => g.Student.Id == studentId && g.Course.Id == courseId);
            if (existingGrade is not null)
            {
                throw new GradeConflictException(studentId, courseId);
            }

            Grade grade = new ()
            {
                Score = requestDto.Score,
                Student = student,
                Course = course,
            };

            _dataContext.Grades.Add(grade);
            await _dataContext.SaveChangesAsync();

            GradeResponseDto responseDto = _mapper.Map<GradeResponseDto>(grade)!;
            return responseDto;
        }

        public async Task<GradeResponseDto> GetGrade(long studentId, long courseId)
        {
            Grade grade = await FindGradeByIdsOrThrow(studentId, courseId);

            GradeResponseDto responseDto = _mapper.Map<GradeResponseDto>(grade)!;
            return responseDto;
        }

        public async Task<GradeResponseDto> UpdateGrade(GradeRequestDto requestDto, long studentId, long courseId)
        {
            Grade grade = await FindGradeByIdsOrThrow(studentId, courseId);

            grade.Score = requestDto.Score;
            await _dataContext.SaveChangesAsync();

            GradeResponseDto responseDto = _mapper.Map<GradeResponseDto>(grade)!;
            return responseDto;
        }

        public async Task DeleteGrade(long studentId, long courseId)
        {
            Grade grade = await FindGradeByIdsOrThrow(studentId, courseId);

            _dataContext.Grades.Remove(grade);
            await _dataContext.SaveChangesAsync();
        }

        public Task<StudentGradesResponseDto> GetGradesByStudentId(long studentId)
        {
            throw new NotImplementedException();
        }

        public Task<CourseGradesResponseDto> GetGradesByCourseId(long courseId)
        {
            throw new NotImplementedException();
        }

        // ----------------------------- Private Methods -----------------------------

        private async Task<Grade> FindGradeByIdsOrThrow(long studentId, long courseId)
        {
            Grade? grade = await _dataContext.Grades
                .Include(g => g.Student)
                .Include(g => g.Course)
                .FirstOrDefaultAsync(g => g.Student.Id == studentId && g.Course.Id == courseId);

            if (grade is null)
            {
                throw new GradeNotFoundException(studentId, courseId);
            }

            return grade;
        }

        private async Task<Student> FindStudnetByIdOrThrow(long studentId)
        {
            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == studentId);
            if (student is null)
            {
                throw new StudentNotFoundException(studentId);
            }
            return student;
        }

        private async Task<Course> FindCourseByIdOrThrow(long courseId)
        {
            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course is null)
            {
                throw new CourseNotFoundException(courseId);
            }
            return course;
        }

    }
}
