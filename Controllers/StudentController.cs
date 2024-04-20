using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Data;
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

        [HttpPost]
        public ActionResult<Student> PostStudent([FromBody] Student student)
        {
            return Ok(_studentService.SaveStudent(student));
        }
    }
}
