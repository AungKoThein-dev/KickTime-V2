using KickTime.Core.DTOs.Booking;
using KickTime.Core.Entities;

namespace KickTime.Application.Booking.Mappings;

public static class BookingMappings
{
    public static BookingEntity ToEntity(
        this CreateBookingRequest request,
        long userId)
    {
        return new BookingEntity
        {
            UserId = userId,
            CourtId = request.CourtId,
            StartTime = request.StartTime,
            EndTime = request.EndTime,
            IsCancelled = false
        };
    }

    public static BookingResponse ToResponse(
        this BookingEntity booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            UserId = booking.UserId,
            CourtId = booking.CourtId,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime,
            IsCancelled = booking.IsCancelled
        };
    }
}