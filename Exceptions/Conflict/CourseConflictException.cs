using Newtonsoft.Json.Linq;

namespace SchoolAPI.Exceptions.Conflict
{
    public class CourseConflictException : ConflictException
    {
        public CourseConflictException(string field, string value) :
            base($"Course with {field} {value} already exists")
        {
        }
    }
}
