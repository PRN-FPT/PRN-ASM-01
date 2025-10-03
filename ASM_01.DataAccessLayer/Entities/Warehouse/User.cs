using System.ComponentModel.DataAnnotations;

namespace ASM_01.DataAccessLayer.Entities.Warehouse;

public class User
{
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Role { get; set; } = null!; // DISTRIBUTOR or DEALER

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLoginAt { get; set; }
}
