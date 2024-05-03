using Microsoft.EntityFrameworkCore;
using SchoolAPI.Data;
using SchoolAPI.Exceptions.Conflict;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Services.Impl
{
    public class StudentService : IStudentService
    {

        private readonly DataContext _context;
        private readonly IEntityFinder<Student, long> _studentFinder;
        private readonly IEntityFinder<Course, long> _courseFinder;

        public StudentService(DataContext context, IEntityFinder<Student, long> studentFinder, IEntityFinder<Course, long> courseFinder)
        {
            _context = context;
            _studentFinder = studentFinder;
            _courseFinder = courseFinder;
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
            Student student = await _studentFinder.FindEntityByIdOrThrow(id);
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
                ThrowStudentEmailConflict(requestDto.Email);
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

        public async Task<StudentResponseDto> UpdateStudent(long id, StudentRequestDto requestDto)
        {
            Student student = await _studentFinder.FindEntityByIdOrThrow(id);

            // If email exists and does not belong to same studnet, throw conflict
            Student? studentWithSameEmail = await GetStudentByEmail(requestDto.Email);
            if (studentWithSameEmail is not null && studentWithSameEmail.Id != student.Id)
            {
                ThrowStudentEmailConflict(requestDto.Email);
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
            Student student = await _studentFinder.FindEntityByIdOrThrow(id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<StudentCoursesResponseDto> EnrollInCourse(long studentId, long courseId)
        {
            Student student = await GetStudentByIdWithCoursesOrThrow(studentId);
            Course course = await _courseFinder.FindEntityByIdOrThrow(courseId);

            student.Courses.Add(course);
            await _context.SaveChangesAsync();

            HashSet<CourseResponseDto> courseDtos = student.Courses
                .Select(c => new CourseResponseDto()
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Description = c.Description
                }).ToHashSet();

            StudentCoursesResponseDto responseDto = new()
            {
                Id = studentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                Courses = courseDtos
            };

            return responseDto;
        }

        public async Task<StudentCoursesResponseDto> UnenrollInCourse(long studentId, long courseId)
        {
            Student student = await GetStudentByIdWithCoursesOrThrow(studentId);
            Course course = await _courseFinder.FindEntityByIdOrThrow(courseId);

            // TODO: Check if student contains course and if not, throw StudentNotEnrolledException.
            student.Courses.Remove(course);
            await _context.SaveChangesAsync();

            HashSet<CourseResponseDto> courseDtos = student.Courses
                .Select(c => new CourseResponseDto()
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Description = c.Description
                }).ToHashSet();

            StudentCoursesResponseDto responseDto = new()
            {
                Id = studentId,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                BirthDate = student.BirthDate,
                Courses = courseDtos
            };

            return responseDto;
        }

        public async Task<List<CourseResponseDto>> GetEnrolledCourses(long id)
        {
            Student student = await GetStudentByIdWithCoursesOrThrow(id);
            List<CourseResponseDto> courseDtos = student.Courses
                .Select(c => new CourseResponseDto
                {
                    Id = c.Id,
                    Code = c.Code,
                    Name = c.Name,
                    Description = c.Description
                })
                .ToList();
            return courseDtos;
        }

        // -------------------------------- Private Methods --------------------------------
        private async Task<Student?> GetStudentByEmail(string email)
        {
            Student? student = await _context.Students.FirstOrDefaultAsync(student => student.Email == email);
            return student;
        }

        private async Task<Student> GetStudentByIdWithCoursesOrThrow(long id)
        {
            Student? student = await _context.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                ThrowNotFoundById(id);
            }
            return student!;
        }

        private static void ThrowNotFoundById(long id)
        {
            Dictionary<string, List<string>> errors = new()
                {
                    { "id", [$"{id}"] }
                };
            throw new NotFoundException(errors, $"Student with id {id} not found");
        }

        private static void ThrowStudentEmailConflict(string email)
        {
            Dictionary<string, List<string>> errors = new()
                {
                    { "email", [$"{email}"] }
                };
            throw new ConflictException(errors, $"Student with email {email} already exists");
        }

    }
}
