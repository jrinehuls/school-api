
namespace SchoolAPI.Exceptions.NotFound
{
    public class NotFoundException : ApiException
    {
        public NotFoundException(string field, string value)
            : base(field, value, string.Format("{0} with the value {1} not found", field, value))
        {
        }
    }
}
