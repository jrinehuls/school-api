using SchoolAPI.Models.DTOs.Grade;

namespace SchoolAPI.Services
{
    public interface IGradeService
    {
        public Task<GradeResponseDto> CreateGrade(GradeRequestDto requestDto, long studentId, long courseId);
        public Task<GradeResponseDto> GetGrade(long studentId, long courseId);
        public Task<GradeResponseDto> UpdateGrade(GradeRequestDto requestDto, long studentId, long courseId);
        public Task DeleteGrade(long studentId, long courseId);
    }
}
