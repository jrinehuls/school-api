namespace SchoolAPI.Exceptions.Conflict
{
    public abstract class ConflictException : ApiException
    {

        public ConflictException(string message) : base(message)
        {
            _statusCode = 409;
        }

    }
}
