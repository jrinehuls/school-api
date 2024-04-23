using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services
{
    public interface IStudentService
    {
        public List<StudentResponseDto> GetStudents();
        public StudentResponseDto GetStudentById(long id);
        public StudentResponseDto SaveStudent(StudentRequestDto student);
        public StudentResponseDto UpdateStudent(long id, StudentRequestDto student);
        public void DeleteStudent(long id);
    }
}
