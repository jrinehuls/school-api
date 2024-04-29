namespace SchoolAPI.Exceptions.Conflict
{
    public class ConflictException : ApiException
    {

        public ConflictException(string field, string value)
            : base(field, value, string.Format("{0} with the value {1} already in use", field, value)) {

        }

    }
}
