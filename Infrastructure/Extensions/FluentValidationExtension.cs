using FluentValidation;
using FluentValidation.Results;
using POVO.Backend.Infrastructure.Exceptions;

namespace POVO.Backend.Infrastructure.Extensions
{
    public static class FluentValidationExtension
    {
        public static ValidationResult ValidateAndThrowEntityValidation<T>(this IValidator<T> validator, T instance,
            string ruleSet = null)
        {
            var validationResult = validator.Validate(instance);

            if (!validationResult.IsValid)
            {
                throw new EntityValidationErrorException(validationResult.Errors);
            }

            return validationResult;
        }
    }
}