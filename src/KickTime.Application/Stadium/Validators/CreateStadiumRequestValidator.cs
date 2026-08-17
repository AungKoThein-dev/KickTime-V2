using FluentValidation;
using KickTime.Core.DTOs.Stadium;

namespace KickTime.Application.Stadium.Validators;

public sealed class CreateStadiumRequestValidator
    : AbstractValidator<CreateStadiumRequest>
{
    public CreateStadiumRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Location)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}