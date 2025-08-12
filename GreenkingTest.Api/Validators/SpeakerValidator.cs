namespace GreenkingTest.Api.Validators;

using FluentValidation;
using DataTransferObjects;

public class SpeakerValidator : AbstractValidator<SpeakerDto>

{
    public SpeakerValidator()
    {
        
        RuleFor(x => x.FirstName).Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("First name is required.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Invalid email format.");

        RuleFor(x => x.Experience).NotNull();
    }
}