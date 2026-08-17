namespace KickTime.Core.Entities
{
    public class StadiumEntity : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}
