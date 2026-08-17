namespace KickTime.Core.Entities;

public sealed class CourtEntity : BaseEntity
{
    public long StadiumId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}