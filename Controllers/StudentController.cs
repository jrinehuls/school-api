using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Exceptions.NotFound;
using SchoolAPI.Models.Entites;
using SchoolAPI.Services;
using System.Net.Mime;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet("All")]
        [ProducesResponseType<IEnumerable<Student>>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(_studentService.GetStudents());
        }

        [HttpGet("{id:long:min(0)}")]
        [ProducesResponseType<Student>(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<Student> GetStudent([FromRoute] long id)
        {
            Student? student = _studentService.GetStudentById(id);
            if (student == null)
            {
                return NotFound($"{{\n  \"message\": \"Student with id {id} not found\"\n}}");
            }
            return Ok(_studentService.GetStudents());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Produces(MediaTypeNames.Application.Json)]
        [Consumes(MediaTypeNames.Application.Json)]
        public ActionResult<Student> PostStudent([FromBody] Student student)
        {
            return Created(string.Empty, _studentService.SaveStudent(student));
        }

        [HttpPut("{id:long:min(0)}")]
        [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public ActionResult<Student> UpdateStudent([FromRoute] long id, [FromBody] Student updatedStudent)
        {
            // Need to handle situations here
            try
            {
                return Ok(_studentService.GetStudentById(id));
            }
            catch (StudentNotFoundException e)
            {
                return NotFound($"{{\n\t\"message\": \"{e.Message}\"\n}}");
            }
        }

        [HttpDelete("{id:long:min(0)}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType<string>(StatusCodes.Status404NotFound)]
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
                return NotFound($"{{\n\t\"message\": \"{e.Message}\"\n}}");
            }
        }
    }
}
