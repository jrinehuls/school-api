using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;
using SchoolAPI.Services;

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
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(_studentService.GetStudents());
        }

        [HttpGet("{id:long}")]
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
        public ActionResult<Student> PostStudent([FromBody] Student student)
        {
            return Ok(_studentService.SaveStudent(student));
        }
    }
}
