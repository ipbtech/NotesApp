using FluentValidation;
using NotesApp.Auth.Dto;

namespace NotesApp.Auth.Validation
{
    public class ChangePasswordValidator : AbstractValidator<ChangePasswordDto>
    {
        public ChangePasswordValidator()
        {
            RuleFor(p => p.OldPassword)
                .NotNull().NotEmpty().WithMessage("Your password cannot be empty")
                .NotEqual(p => p.NewPassword).WithMessage("Passwords mustn't match");


            RuleFor(p => p.NewPassword)
                .NotNull().NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number")
                .Matches(@"[\!\?\%\$\*]+").WithMessage("Your password must contain at least one (!?$%*)");
        }
    }
}
