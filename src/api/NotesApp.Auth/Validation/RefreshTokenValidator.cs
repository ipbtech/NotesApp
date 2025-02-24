using FluentValidation;
using NotesApp.Auth.Dto;

namespace NotesApp.Auth.Validation;

public class RefreshTokenValidator : AbstractValidator<RefreshTokenRequestDto>
{
    public RefreshTokenValidator() : base()
    {
        RuleFor(e => e.RefreshToken)
            .NotNull().NotEmpty().WithMessage("Can't be empty");

        RuleFor(e => e.UserId)
            .NotNull().NotEmpty().WithMessage("Can't be empty");
    }
}