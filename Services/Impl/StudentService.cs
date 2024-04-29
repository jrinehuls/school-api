using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entites;
using System.Runtime.CompilerServices;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {

        private readonly DataContext _context;

        public StudentService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<StudentResponseDto>> GetStudents()
        {
            List<StudentResponseDto> dtos = await _context.Students.Select(s => new StudentResponseDto()
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                BirthDate = s.BirthDate
            }).ToListAsync();
            return dtos;
        }

        public async Task<StudentResponseDto> GetStudentById(long id)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
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

        public async Task<StudentResponseDto> SaveStudent(StudentRequestDto requestDto)
        {
            if (await GetStudentByEmail(requestDto.Email) is not null)
            {
                throw new ConflictException(nameof(requestDto.Email), requestDto.Email);
            }
            Student student = new ()
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                Email = requestDto.Email,
                BirthDate = requestDto.BirthDate
            };
            _context.Students.Add(student);
            await _context.SaveChangesAsync();

            Student createdStudent = await _context.Students.FirstAsync(s => s.Id == student.Id);
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

        public async Task<StudentResponseDto> UpdateStudent(long id, StudentRequestDto requestDto)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }

            student.FirstName = requestDto.FirstName;
            student.LastName = requestDto.LastName;
            student.Email = requestDto.Email;
            student.BirthDate = requestDto.BirthDate;

            await _context.SaveChangesAsync();

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

        public async Task DeleteStudent(long id)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
            {
                throw new StudentNotFoundException(id);
            }
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        private async Task<Student?> GetStudentByEmail(string email)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(student => student.Email == email);
            return student;
        }
    }
}
