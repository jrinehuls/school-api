using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Grade;
using SchoolAPI.Models.DTOs.Student;

namespace SchoolAPI.Services
{
    public interface IGradeService
    {
        public Task<GradeResponseDto> CreateGrade(GradeRequestDto requestDto, long studentId, long courseId);
        public Task<GradeResponseDto> GetGrade(long studentId, long courseId);
        public Task<GradeResponseDto> UpdateGrade(GradeRequestDto requestDto, long studentId, long courseId);
        public Task DeleteGrade(long studentId, long courseId);
        public Task<StudentGradesResponseDto> GetGradesByStudentId(long studentId);
        public Task<CourseGradesResponseDto> GetGradesByCourseId(long courseId);
    }
}
