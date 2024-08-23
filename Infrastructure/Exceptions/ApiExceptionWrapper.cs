namespace POVO.Backend.Infrastructure.Exceptions
{
    public class ApiExceptionWrapper
    {
        public string ExceptionName { get; set; }
        public string ExceptionMessage { get; set; }

        public ICollection<string> Callstack { get; set; }

        public ICollection<string> Errors { get; set; }
    }
}