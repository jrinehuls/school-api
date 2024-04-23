using SchoolAPI.Data;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {

        public List<StudentResponseDto> GetStudents()
        {
            IEnumerable<StudentResponseDto> dtos = StudentRepository.Students.Select(s => new StudentResponseDto()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                Address = s.Address
            });
            return dtos.ToList();
        }

        public StudentResponseDto GetStudentById(long id)
        {
            Student? student = StudentRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student is null)
            {
                throw new StudentNotFoundException(id);
            }
            StudentResponseDto studentDto = new ()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                Address = student.Address
            };
            return studentDto;
        }

        public StudentResponseDto SaveStudent(StudentRequestDto requestDto)
        {
            long newId = StudentRepository.Students.Max(s => s.Id) + 1;
            Student student = new ()
            {
                Id = newId,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                Address = requestDto.Address
            };
            StudentRepository.Students.Add(student);

            Student createdStudent = StudentRepository.Students.FirstOrDefault(s => s.Id == student.Id)!;
            StudentResponseDto studentDto = new ()
            {
                Id = createdStudent.Id,
                FirstName = createdStudent.FirstName,
                LastName = createdStudent.LastName,
                Email = createdStudent.Email,
                Address = createdStudent.Address
            };
            return studentDto;
        }

        public StudentResponseDto UpdateStudent(long id, StudentRequestDto requestDto)
        {
            Student? student = StudentRepository.Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            int index = StudentRepository.Students.IndexOf(student);
            student.FirstName = requestDto.FirstName;
            student.LastName = requestDto.LastName;
            student.Email = requestDto.Email;
            student.Address = requestDto.Address;
            StudentRepository.Students[index] = student;

            Student updatedStudent = StudentRepository.Students[index];
            StudentResponseDto responseDto = new()
            {
                Id = updatedStudent.Id,
                FirstName = updatedStudent.FirstName,
                LastName = updatedStudent.LastName,
                Email = updatedStudent.Email,
                Address = updatedStudent.Address
            };

            return responseDto;
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
