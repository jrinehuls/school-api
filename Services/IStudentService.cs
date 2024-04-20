using SchoolAPI.Models;

namespace SchoolAPI.Services
{
    public interface IStudentService
    {
        public List<Student> GetStudents();
        public Student GetStudentById(long id);
        public Student SaveStudent(Student student);
        public Student UpdateStudent(long id, Student student);
        public void DeleteStudent(long id);
    }
}
