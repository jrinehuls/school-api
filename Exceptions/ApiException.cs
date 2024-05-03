using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAPI.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected int _statusCode;
        protected readonly Dictionary<string, List<string>>? _errors;

        public ApiException(Dictionary<string, List<string>> errors, string message) : base(message)
        {
            _errors = errors;
        }

        public int StatusCode => _statusCode;
        public Dictionary<string, List<string>>? Errors => _errors;

    }
}
