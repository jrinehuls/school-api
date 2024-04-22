namespace SchoolAPI.Exceptions.NotFound
{
    public class StudentNotFoundException : NotFoundException
    {
        public StudentNotFoundException(long id) : base($"Student with id: {id} not found")
        {
        }
    }
}
