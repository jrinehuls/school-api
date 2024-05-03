using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Filters;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Services;
using SchoolAPI.Services.Impl;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [StudentFilter]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private const string getStudentById = "GetStudentById";

        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost(Name = "CreateStudent")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponseDto>> PostStudent([FromBody] StudentRequestDto studentDto)
        {
            StudentResponseDto responseDto = await _studentService.SaveStudent(studentDto);
            return CreatedAtRoute(getStudentById, new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("all", Name = "GetAllStudents")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<List<StudentResponseDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<StudentResponseDto>>> GetAllStudents()
        {
            return Ok(await _studentService.GetStudents());
        }

        [HttpGet("{id:long:min(1)}", Name = getStudentById)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponseDto>> GetStudentById([FromRoute] long id)
        {
            return Ok(await _studentService.GetStudentById(id));
        }

        [HttpPut("{id:long:min(1)}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentResponseDto>> UpdateStudent([FromRoute] long id, [FromBody] StudentRequestDto studentDto)
        {
            return Ok(await _studentService.UpdateStudent(id, studentDto));
        }

        [HttpDelete("{id:long:min(1)}", Name = "DeleteStudent")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteStudent([FromRoute] long id)
        {
            await _studentService.DeleteStudent(id);
            return NoContent();
        }

        [HttpPatch("{studentId:long:min(1)}/enroll-in-course/{courseId:long:min(1)}", Name = "EnrollInCourse")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentCoursesResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentCoursesResponseDto>> EnrollInCourse([FromRoute] long studentId, [FromRoute] long courseId)
        {
            return Ok(await _studentService.EnrollInCourse(studentId, courseId));
        }

        [HttpPatch("{studentId:long:min(1)}/unenroll-in-course/{courseId:long:min(1)}", Name = "UnenrollInCourse")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentCoursesResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentCoursesResponseDto>> UnenrollInCourse([FromRoute] long studentId, [FromRoute] long courseId)
        {
            return Ok(await _studentService.UnenrollInCourse(studentId, courseId));
        }

        [HttpGet("{id:long:min(1)}/courses", Name = "GetEnrolledCourses")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<List<CourseResponseDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<ErrorResponse>(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CourseResponseDto>>> GetEnrolledCourses([FromRoute] long id)
        {
            return Ok(await _studentService.GetEnrolledCourses(id));
        }

    }
}
