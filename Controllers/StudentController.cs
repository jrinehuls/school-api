using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolAPI.Models;

namespace SchoolAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        [HttpGet("All")]
        public ActionResult<List<Student>> getStudents()
        {
            List<Student> students = new List<Student>
            {
                 new Student {Id = 1, FirstName = "Jamal", LastName = "Bascome"}
            };

            return Ok(students);
        }
    }
}
