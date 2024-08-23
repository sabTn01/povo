namespace POVO.Backend.Infrastructure.Exceptions
{
    public class ExceptionName
    {
        public string _name;

        public ExceptionName(string name)
        {
            _name = name;
        }

        public string GetUppercaseName()
        {
            return _name.ToUpper();
        }
    }

    public class ExceptionNameFactory
    {
        public class BadRequest
        {
            public static ExceptionName Generic => new ExceptionName("BAD_REQUEST_GENERIC");

            public static ExceptionName IdNotProvided => new ExceptionName("BAD_REQUEST_ID_NOT_PROVIDED");
            public static ExceptionName IdProvidedOnCreate => new ExceptionName("BAD_REQUEST_ID_PROVIDED_ON_CREATE");
        }
    }
}