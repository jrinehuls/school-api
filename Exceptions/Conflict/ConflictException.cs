namespace SchoolAPI.Exceptions.Conflict
{
    public class ConflictException : ApiException
    {

        public ConflictException(Dictionary<string, List<string>> errors, string message)
            : base(errors, message)
        {
            _statusCode = 409;
        }

    }
}
