using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Filters;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.DTOs.Grade;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [GradeFilter]
    [Route("api/[controller]")]
    public class GradeController : ControllerBase
    {
        private const string getGrade = "GetGrade";

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

            return CreatedAtRoute(getGrade, new { studentId, courseId }, null);
        }

        [HttpGet("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = getGrade)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<GradeResponseDto>(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GradeResponseDto>> GetGrade([FromRoute] long studentId, [FromRoute] long courseId)
        {
            return Ok();
        }

        [HttpPut("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = "UpdateGrade")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType<GradeResponseDto>(200)]
        [ProducesResponseType<ValidationProblemDetails>(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType<ErrorResponse>(409)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<GradeResponseDto>> UpdateGrade([FromBody] GradeRequestDto grade,
            [FromRoute] long studentId, [FromRoute] long courseId)
        {
            return Ok();
        }

        [HttpDelete("student/{studentId:long:min(1)}/course/{courseId:long:min(1)}", Name = "DeleteGrade")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType<ErrorResponse>(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> DeleteGrade([FromRoute] long studentId, [FromRoute] long courseId)
        {
            return Ok();
        }
    }
}
