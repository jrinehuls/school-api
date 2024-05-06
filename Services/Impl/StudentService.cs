using AutoMapper;
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

        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public StudentService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<StudentResponseDto>> GetStudents()
        {
            List<StudentResponseDto> dtos = await _dataContext.Students
                .Select(s => _mapper.Map<StudentResponseDto>(s)!)
                .ToListAsync();
            return dtos;
        }

        public async Task<StudentResponseDto> GetStudentById(long id)
        {
            Student student = await FindByIdOrThrow(id);
            StudentResponseDto studentDto = _mapper.Map<StudentResponseDto>(student)!;
            return studentDto;
        }

        public async Task<StudentResponseDto> SaveStudent(StudentRequestDto requestDto)
        {
            if (await GetStudentByEmail(requestDto.Email) is not null)
            {
                throw new StudentConflictException("email", requestDto.Email);
            }

            Student student = _mapper.Map<Student>(requestDto)!;
            _dataContext.Students.Add(student);
            await _dataContext.SaveChangesAsync();

            StudentResponseDto studentDto = _mapper.Map<StudentResponseDto>(student)!;
            return studentDto;
        }

        public async Task<StudentResponseDto> UpdateStudent(long id, StudentRequestDto requestDto)
        {
            Student student = await FindByIdOrThrow(id);
            Student? studentWithSameEmail = await GetStudentByEmail(requestDto.Email);
            // If email exists and does not belong to same student, throw conflict
            if (studentWithSameEmail is not null && studentWithSameEmail.Id != student.Id)
            {
                throw new StudentConflictException("email", studentWithSameEmail.Email);
            }

            student.FirstName = requestDto.FirstName;
            student.LastName = requestDto.LastName;
            student.Email = requestDto.Email;
            student.BirthDate = requestDto.BirthDate;
            
            await _dataContext.SaveChangesAsync();

            StudentResponseDto responseDto = _mapper.Map<StudentResponseDto>(student)!;

            return responseDto;
        }

        public async Task DeleteStudent(long id)
        {
            Student student = await FindByIdOrThrow(id);
            _dataContext.Students.Remove(student);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<StudentCoursesResponseDto> EnrollInCourse(long studentId, long courseId)
        {
            Student student = await FindByIdWithCoursesOrThrow(studentId);

            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course is null)
            {
                throw new CourseNotFoundException(courseId);
            }

            if (student.Courses.Contains(course))
            {
                throw new StudentAlreadyEnrolledException(studentId, courseId);
            }

            student.Courses.Add(course);
            await _dataContext.SaveChangesAsync();

            StudentCoursesResponseDto responseDto = _mapper.Map<StudentCoursesResponseDto>(student)!;

            return responseDto;
        }

        public async Task<StudentCoursesResponseDto> UnenrollInCourse(long studentId, long courseId)
        {
            Student student = await FindByIdWithCoursesOrThrow(studentId);

            Course? course = await _dataContext.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
            if (course is null)
            {
                throw new CourseNotFoundException(courseId);
            }

            if (!student.Courses.Contains(course))
            {
                throw new StudentNotEnrolledException(studentId, courseId);
            }

            student.Courses.Remove(course);
            await _dataContext.SaveChangesAsync();

            StudentCoursesResponseDto responseDto = _mapper.Map<StudentCoursesResponseDto>(student)!;

            return responseDto;
        }

        public async Task<List<CourseResponseDto>> GetEnrolledCourses(long id)
        {
            Student student = await FindByIdWithCoursesOrThrow(id);

            List<CourseResponseDto> courseDtos = student.Courses
                .Select(c => _mapper.Map<CourseResponseDto>(c)!)
                .ToList();

            return courseDtos;
        }

        // -------------------------------- Private Methods --------------------------------
        private async Task<Student?> GetStudentByEmail(string email)
        {
            Student? student = await _dataContext.Students.FirstOrDefaultAsync(student => student.Email == email);
            return student;
        }

        private async Task<Student> FindByIdOrThrow(long id)
        {
            Student? student = await _dataContext.Students.FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                throw new StudentNotFoundException(id);
            }
            return student;
        }

        private async Task<Student> FindByIdWithCoursesOrThrow(long id)
        {
            Student? student = await _dataContext.Students
                .Include(s => s.Courses)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (student is null)
            {
                throw new StudentNotFoundException(id);
            }
            return student;
        }

    }
}
