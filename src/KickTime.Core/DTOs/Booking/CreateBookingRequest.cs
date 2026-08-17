namespace KickTime.Core.DTOs.Booking;

public class CreateBookingRequest
{
    public long CourtId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}