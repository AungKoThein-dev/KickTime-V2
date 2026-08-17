namespace KickTime.Core.DTOs.Booking;

public class BookingResponse
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long CourtId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsCancelled { get; set; }
}