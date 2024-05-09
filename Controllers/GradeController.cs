using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Filters;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.DTOs.Course;
using SchoolAPI.Models.DTOs.Grade;
using SchoolAPI.Models.DTOs.Student;
using SchoolAPI.Services;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [GradeFilter]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private const string getGrade = "GetGrade";
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        [HttpPost("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = "CreateGrade")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<GradeResponseDto>(201)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GradeResponseDto>> CreateGrade([FromBody] GradeRequestDto grade,
            [FromRoute] long studentId, [FromRoute] long courseId)
        {
            GradeResponseDto responseDto = await _gradeService.CreateGrade(grade, studentId, courseId);
            return CreatedAtRoute(getGrade, new { studentId, courseId }, responseDto);
        }

        [HttpGet("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = getGrade)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<GradeResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GradeResponseDto>> GetGrade([FromRoute] long studentId, [FromRoute] long courseId)
        {
            GradeResponseDto responseDto = await _gradeService.GetGrade(studentId, courseId);
            return Ok(responseDto);
        }

        [HttpPut("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = "UpdateGrade")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<GradeResponseDto>(200)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GradeResponseDto>> UpdateGrade([FromBody] GradeRequestDto grade,
            [FromRoute] long studentId, [FromRoute] long courseId)
        {
            GradeResponseDto responseDto = await _gradeService.UpdateGrade(grade, studentId, courseId);
            return Ok(responseDto);
        }

        [HttpDelete("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = "DeleteGrade")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteGrade([FromRoute] long studentId, [FromRoute] long courseId)
        {
            await _gradeService.DeleteGrade(studentId, courseId);
            return NoContent();
        }

        [HttpGet("student/{studentId:long:min(1)}", Name = "GetStudentGrades")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<StudentGradesResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentGradesResponseDto>> GetStudentGrades([FromRoute] long studentId)
        {
            StudentGradesResponseDto responseDto = await _gradeService.GetGradesByStudentId(studentId);
            return Ok(responseDto);
        }

        [HttpGet("course/{courseId:long:min(1)}", Name = "GetCourseGrades")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<CourseGradesResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<StudentGradesResponseDto>> GetCourseGrades([FromRoute] long courseId)
        {
            CourseGradesResponseDto responseDto = await _gradeService.GetGradesByCourseId(courseId);
            return Ok(responseDto);
        }
    }
}
