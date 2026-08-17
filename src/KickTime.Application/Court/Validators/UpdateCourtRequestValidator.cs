using FluentValidation;
using KickTime.Core.DTOs.Court;

namespace KickTime.Application.Court.Validators;

public sealed class UpdateCourtRequestValidator
    : AbstractValidator<UpdateCourtRequest>
{
    public UpdateCourtRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Court name is required.")
            .MaximumLength(100)
            .WithMessage("Court name must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500)
            .WithMessage("Court description must not exceed 500 characters.");
    }
}