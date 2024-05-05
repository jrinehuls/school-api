namespace SchoolAPI.Exceptions.NotFound
{
    public class CourseNotFoundException : NotFoundException
    {
        public CourseNotFoundException(long id) : base($"Course with id {id} not found")
        {
        }
    }
}
