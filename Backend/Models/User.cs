using System.ComponentModel.DataAnnotations.Schema;
using Backend.Enums;
using Backend.Extensions;

namespace Backend.Models;

public class User
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    [Column(nameof(Role))]
    public string RoleStr
    {
        get { return Role.ToString(); }
        set { Role = value.ParseEnum<UserRole>(); }
    }

    [NotMapped]
    public UserRole Role { get; set; }
}