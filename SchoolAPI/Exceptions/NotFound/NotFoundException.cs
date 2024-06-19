
namespace SchoolAPI.Exceptions.NotFound
{
    public abstract class NotFoundException : ApiException
    {
        
        public NotFoundException(string message) : base(message)
        {
            _statusCode = 404;
        }

    }
}
