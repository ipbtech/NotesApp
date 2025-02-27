using FluentValidation;
using NotesApp.Dto;

namespace NotesApp.Auth.Validation
{
    public class LoginValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginValidator()
        {
            RuleFor(e => e.Email)
                .NotNull().NotEmpty().WithMessage("Your email cannot be empty")
                .EmailAddress().WithMessage("It doesn't look like valid email address");

            RuleFor(p => p.Password)
                .NotNull().NotEmpty().WithMessage("Your password cannot be empty");
        }
    }
}
