using System.Data;

namespace KickTime.Core.Entities;

public class User : BaseEntity
{
   // public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string? Phone { get; set; }

    public string PasswordHash { get; set; } = string.Empty;

    public int RoleId { get; set; }

    public bool IsActive { get; set; }

    //public DateTime CreatedAt { get; set; }

    //public DateTime UpdatedAt { get; set; }

    //public Role? Role { get; set; }
}