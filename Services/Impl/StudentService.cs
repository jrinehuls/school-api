using SchoolAPI.Data;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {

        public List<Student> GetStudents()
        {
            return StudentRepository.Students;
        }


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

        public Student SaveStudent(Student student)
        {
            StudentRepository.Students.Add(student);
            return StudentRepository.Students.FirstOrDefault(s => s.Id == student.Id)!;
        }

        public Student UpdateStudent(long id, Student updatedStudent)
        {
            Student? student = StudentRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            int index = StudentRepository.Students.IndexOf(student);
            StudentRepository.Students[index] = updatedStudent;

            return StudentRepository.Students[index];
        }

        public void DeleteStudent(long id)
        {
            Student? student = StudentRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            StudentRepository.Students.Remove(student);
        }
    }
}
