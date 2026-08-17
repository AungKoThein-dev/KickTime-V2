namespace KickTime.Core.DTOs.Court;

public sealed class CourtResponse
{
    public long Id { get; set; }

    public long StadiumId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}