using SchoolAPI.Data;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {
        public Student GetStudentById(long id)
        {
            try {
                Student student = StudentRepository.Students.FirstOrDefault(s => s.Id == id);
                return student;
            } catch (ArgumentNullException e)
            {
                return null;
            }
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
