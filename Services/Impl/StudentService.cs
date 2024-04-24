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
                BirthDate = s.BirthDate
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
                BirthDate = student.BirthDate
            };
            return studentDto;
        }

        public StudentResponseDto SaveStudent(StudentRequestDto requestDto)
        {
            long newId = 1;
            if (StudentRepository.Students.Count > 0)
            {
                newId = StudentRepository.Students.Max(s => s.Id) + 1;
            }

            Student student = new ()
            {
                Id = newId,
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                BirthDate = requestDto.BirthDate
            };
            StudentRepository.Students.Add(student);

            Student createdStudent = StudentRepository.Students.FirstOrDefault(s => s.Id == student.Id)!;
            StudentResponseDto studentDto = new ()
            {
                Id = createdStudent.Id,
                FirstName = createdStudent.FirstName,
                LastName = createdStudent.LastName,
                Email = createdStudent.Email,
                BirthDate = createdStudent.BirthDate
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

            student.FirstName = requestDto.FirstName;
            student.LastName = requestDto.LastName;
            student.Email = requestDto.Email;
            student.BirthDate = requestDto.BirthDate;

            StudentResponseDto responseDto = new()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate
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
