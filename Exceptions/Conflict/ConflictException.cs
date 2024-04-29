namespace SchoolAPI.Exceptions.Conflict
{
    public class ConflictException : Exception
    {

        public ConflictException(string field, string value)
            : base(string.Format("{0} with the value {1} already in use", field, value)) {

        }
    }
}
