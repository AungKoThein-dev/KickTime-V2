namespace KickTime.Core.DTOs.Court;

public sealed class CreateCourtRequest
{
    public long StadiumId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}