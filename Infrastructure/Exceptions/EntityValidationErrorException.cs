using FluentValidation.Results;

namespace POVO.Backend.Infrastructure.Exceptions
{
    public class EntityValidationErrorException : Exception
    {
        public List<string> Errors = new List<string>();

        public EntityValidationErrorException(string message) : base(message)
        {
        }

        public EntityValidationErrorException(IEnumerable<ValidationFailure> errors) : base(
            "Validation errors. See Errors property.")
        {
            errors.ToList().ForEach(e =>
            {
                var newString = $"{e.PropertyName} - {e.ErrorMessage}";

                Errors.Add(newString);
            });
        }
    }
}