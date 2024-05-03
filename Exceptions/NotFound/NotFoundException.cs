
namespace SchoolAPI.Exceptions.NotFound
{
    public class NotFoundException : ApiException
    {
        
        public NotFoundException(Dictionary<string, List<string>> errors, string message)
            : base(errors, message)
        {
            _statusCode = 404;
        }

    }
}
