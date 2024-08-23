using FluentValidation;
using POVO.Backend.Domain.Polls;

namespace POVO.Backend.Application.Polls
{
    public class PollOptionValidator : AbstractValidator<PollOption>
    {
        public PollOptionValidator() 
        {
            RuleFor(po => po.OptionText)
                .NotEmpty().WithMessage("Option Text is required")
                .Length(3,50).WithMessage("Option Text must be between 3 and 50 characters.");
        }
    }
}
