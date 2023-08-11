using System.ComponentModel.DataAnnotations.Schema;
using Backend.Enums;
using Backend.Extensions;
using Backend.Helpers;

namespace Backend.Models;

public class User
{
    public Guid ID { get; set; } = Guid.NewGuid();
    public string Email { get; set; }
    public string Name { get; set; }
    public string NormalizedName { get; set; }
    public string Password { get; set; }
    public string PasswordHash { get; set; }

    [Column(nameof(Role))]
    public string RoleStr
    {
        get => Role.ToString();
        set => Role = value.ParseEnum<UserRole>();
    }

    [NotMapped]
    public UserRole Role { get; set; } = UserRole.Member;

    [NotMapped]
    public BloodGroup BloodGroup { get; set; }

    [Column(nameof(BloodGroup))]
    public string BloodGroupStr
    {
        get => BloodGroupHelper.ToString(BloodGroup);
        set
        {
            _ = value.TryToBloodGroup(out BloodGroup bloodGroup);
            BloodGroup = bloodGroup;
        }
    }

    public string Mobile { get; set; }

    public Guid AddressId { get; set; }
    public Address Address { get; set; }
}
