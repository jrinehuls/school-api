using AutoMapper;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Grade;
using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Models.Entites;

namespace SchoolAPI.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile() {
            // <From, To>
            CreateMap<Student, StudentResponseDto>();
            CreateMap<StudentRequestDto, Student>();
            CreateMap<Student, StudentCoursesResponseDto>();

            CreateMap<Course, CourseResponseDto>();
            CreateMap<CourseRequestDto, Course>();
            CreateMap<Course, CourseStudentsResponseDto>();

            CreateMap<Grade, GradeResponseDto>();
            CreateMap<GradeRequestDto, Grade>();
        }

    }
}
