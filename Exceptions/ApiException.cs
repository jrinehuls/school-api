namespace SchoolAPI.Exceptions
{
    public abstract class ApiException : Exception
    {

        protected string _field;
        protected string _value;

        public ApiException(string field, string value) : base($"Error on {field} with value {value}") {
            _field = field;
            _value = value;
        }

        public ApiException(string field, string value, string message) : base(message)
        {
            _field = field;
            _value = value;
        }

        public string Field => _field;
        public string Value => _value;

    }
}
