using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Filters;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Services;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{
    [ApiController]
    [CourseFilter]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private const string getCourseById = "GetCourseById";
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost(Name = "CreateCourse")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<CourseResponseDto>(201)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CourseResponseDto>> CreateCourse([FromBody] CourseRequestDto requestDto)
        {
            CourseResponseDto responseDto = await _courseService.CreateCourse(requestDto);
            return CreatedAtRoute(getCourseById, new { id = responseDto.Id }, responseDto);
        }

        [HttpGet("All", Name = "GetAllCourses")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<List<CourseResponseDto>>(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<CourseResponseDto>>> GetAllCourses()
        {
            return Ok(await _courseService.GetAllCourses());
        }

        [HttpGet("{id:long:min(1)}", Name = getCourseById)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<CourseResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CourseResponseDto>> GetCourseById([FromRoute] long id)
        {
            return Ok(await _courseService.GetCourseById(id));
        }

        [HttpPut("{id:long:min(1)}", Name = "UpdateStudent")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<CourseResponseDto>(200)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType<ErrorResponse>(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CourseResponseDto>> UpdateCourse([FromRoute] long id, [FromBody] CourseRequestDto requestDto)
        {
            return Ok(await _courseService.UpdateCourse(id, requestDto));
        }

        [HttpDelete("{id:long:min(1)}", Name = "DeleteCourseById")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteCourse([FromRoute] long id)
        {
            await _courseService.DeleteCourse(id);
            return NoContent();
        }

        [HttpPatch("{courseId:long:min(1)}/student/{studentId:long:min(1)}")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<CourseStudentsResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<CourseStudentsResponseDto>> EnrollStudent([FromRoute] long courseId, [FromRoute] long studentId)
        {
            return Ok(await _courseService.EnrollStudent(courseId, studentId));
        }

    }
}
