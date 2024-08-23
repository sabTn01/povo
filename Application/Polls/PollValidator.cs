using FluentValidation;
using POVO.Backend.Domain.Polls;

namespace POVO.Backend.Application.Polls
{
    public class PollValidator : AbstractValidator<Poll>
    {
        public PollValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required.").Length(5,50).WithMessage("Title must be between 5 and 50 characters.");
            RuleFor(p => p.Description).NotEmpty().WithMessage("Description is required.").Length(5, 100).WithMessage("Description must be between 5 and 100 characters.");
            RuleFor(p => p.ExpiryDate).NotEmpty().WithMessage("ExpiryDate is required.").Must(BeInTheFuture).WithMessage("The expiry date must be at least 1 hour in the future.");
            RuleForEach(p => p.Options).SetValidator(new PollOptionValidator());
        }

        private static bool BeInTheFuture(DateTime date)
        {
            return date > DateTime.UtcNow.AddHours(1);
        }
    }
}
