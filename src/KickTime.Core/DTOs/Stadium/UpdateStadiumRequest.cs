namespace KickTime.Core.DTOs.Stadium
{
    public class UpdateStadiumRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
