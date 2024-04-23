using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.DTOs;
using SchoolAPI.Models.Entites;
using SchoolAPI.Services;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private const string getStudentById = "GetStudentById";
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("All", Name = "GetAllStudents")]
        [ProducesResponseType<List<StudentResponseDto>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<List<StudentResponseDto>> GetAllStudents()
        {
            return Ok(_studentService.GetStudents());
        }

        [HttpGet("{id:long:min(0)}", Name = getStudentById)]
        [ProducesResponseType<StudentResponseDto>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<StudentResponseDto> GetStudentById([FromRoute] long id)
        {
            try
            {
                return Ok(_studentService.GetStudentById(id));
            }
            catch (StudentNotFoundException e)
            {
                return NotFound($"{{\n  \"message\": \"{e.Message}\"\n}}");
            }

        }

        [HttpPost(Name = "CreateStudent")]
        [ProducesResponseType<StudentResponseDto>(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<StudentResponseDto> PostStudent([FromBody] StudentRequestDto studentDto)
        {
            StudentResponseDto responseDto = _studentService.SaveStudent(studentDto);
            return CreatedAtRoute(getStudentById, new { id = responseDto.Id }, responseDto);
        }

        [HttpPut("{id:long:min(0)}")]
        [ProducesResponseType(typeof(StudentResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<StudentResponseDto> UpdateStudent([FromRoute] long id, [FromBody] StudentRequestDto studentDto)
        {
            try
            {
                return Ok(_studentService.UpdateStudent(id, studentDto));
            }
            catch (StudentNotFoundException e)
            {
                return NotFound($"{{\n  \"message\": \"{e.Message}\"\n}}");
            }
        }

        [HttpDelete("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult DeleteStudent([FromRoute] long id)
        {
            try
            {
                _studentService.DeleteStudent(id);
                return NoContent();
            }
            catch (StudentNotFoundException e)
            {
                return NotFound($"{{\n  \"message\": \"{e.Message}\"\n}}");
            }
        }
    }
}
