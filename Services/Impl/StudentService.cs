using SchoolAPI.Data;
using SchoolAPI.Models;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {
        public Student GetStudentById(long id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetStudents()
        {
            return StudentRepository.Students;
        }

        public Student SaveStudent(Student student)
        {
            StudentRepository.Students.Add(student);
            return student;
        }

        public Student UpdateStudent(long id, Student student)
        {
            throw new NotImplementedException();
        }

        public void DeleteStudent(long id)
        {
            throw new NotImplementedException();
        }
    }
}
