namespace KickTime.Core.DTOs.Stadium
{
    public class StadiumResponse
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
