using SchoolAPI.Models.Entites;

namespace SchoolAPI.Data
{
    public class StudentRepository
    {
        public static List<Student> Students {  get; set; } = new List<Student>();
    }
}
