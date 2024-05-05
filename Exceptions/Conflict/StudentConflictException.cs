namespace SchoolAPI.Exceptions.Conflict
{
    public class StudentConflictException : ConflictException
    {

        public StudentConflictException(string field, string value) :
            base($"Student with {field} {value} already exists")
        {
        }

    }
}
