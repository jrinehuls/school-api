using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SchoolAPI.Exceptions
{
    public abstract class ApiException : Exception
    {
        protected int _statusCode;

        public ApiException(string message) : base(message)
        {
        }

        public int StatusCode => _statusCode;

    }
}
