using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services
{
    public interface IStudentService
    {
        public Task<List<StudentResponseDto>> GetStudents();
        public Task<StudentResponseDto> GetStudentById(long id);
        public Task<StudentResponseDto> SaveStudent(StudentRequestDto student);
        public Task<StudentResponseDto> UpdateStudent(long id, StudentRequestDto student);
        public Task DeleteStudent(long id);
    }
}
