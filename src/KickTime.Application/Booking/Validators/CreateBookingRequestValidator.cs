using FluentValidation;
using KickTime.Core.DTOs.Booking;

namespace KickTime.Application.Booking.Validators;

public sealed class CreateBookingRequestValidator
    : AbstractValidator<CreateBookingRequest>
{
    public CreateBookingRequestValidator()
    {
        RuleFor(x => x.CourtId)
            .GreaterThan(0)
            .WithMessage("Court id must be greater than zero.");

        RuleFor(x => x.StartTime)
            .NotEmpty()
            .WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotEmpty()
            .WithMessage("End time is required.");

        RuleFor(x => x)
            .Must(x => x.StartTime < x.EndTime)
            .WithMessage("Start time must be earlier than end time.");

        RuleFor(x => x.StartTime)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Booking start time must be in the future.");

        //RuleFor(x => x.StartTime)
        //.Must(BeUtc)
        //.WithMessage("Start time must be in UTC.");

        //RuleFor(x => x.EndTime)
        //    .Must(BeUtc)
        //    .WithMessage("End time must be in UTC.");
    }
}