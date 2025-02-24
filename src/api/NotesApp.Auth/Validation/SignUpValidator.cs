using FluentValidation;
using NotesApp.Auth.Dto;

namespace NotesApp.Auth.Validation;

public class SignUpValidator : AbstractValidator<SignUpRequestDto>
{
    public SignUpValidator()
    {
        RuleFor(e => e.Email)
            .NotNull().NotEmpty().WithMessage("Your email cannot be empty")
            .EmailAddress().WithMessage("It doesn't look like valid email address");

        RuleFor(p => p.Password)
            .NotNull().NotEmpty().WithMessage("Your password cannot be empty")
            .MinimumLength(8).WithMessage("Your password length must be at least 8")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number")
            .Matches(@"[\!\?\%\$\*]+").WithMessage("Your password must contain at least one (!?$%*)");

        RuleFor(e => e.ConfirmPassword)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .Equal(e => e.Password).WithMessage("Passwords should match with each other");
    }
}