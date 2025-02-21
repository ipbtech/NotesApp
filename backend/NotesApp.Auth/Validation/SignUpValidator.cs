using FluentValidation;
using NotesApp.Auth.Dto;

namespace NotesApp.Auth.Validation;

public class SignUpValidator : AuthAbstractValidator<SignUpDto>
{
    public SignUpValidator() : base()
    {
        RuleFor(e => e.ConfirmPassword)
            .NotEmpty().WithMessage("Your password cannot be empty")
            .Equal(e => e.Password).WithMessage("Passwords should match with each other");
    }
}