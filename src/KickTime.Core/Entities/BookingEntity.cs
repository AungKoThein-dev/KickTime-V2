namespace KickTime.Core.Entities;

public class BookingEntity : BaseEntity
{
    public long UserId { get; set; }

    public long CourtId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsCancelled { get; set; }
}