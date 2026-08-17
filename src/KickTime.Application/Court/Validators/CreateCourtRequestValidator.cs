using FluentValidation;
using KickTime.Core.DTOs.Court;

namespace KickTime.Application.Court.Validators;

public sealed class CreateCourtRequestValidator
    : AbstractValidator<CreateCourtRequest>
{
    public CreateCourtRequestValidator()
    {
        RuleFor(x => x.StadiumId)
            .GreaterThan(0)
            .WithMessage("Stadium id must be greater than zero.");

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