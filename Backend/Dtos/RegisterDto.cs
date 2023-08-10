using System.ComponentModel.DataAnnotations;
using Backend.Enums;

namespace Backend.Dtos;

public class RegisterDto
{
    [EmailAddress]
    public string Email { get; set; }

    public string Password { get; set; }

    [EnumDataType(typeof(UserRole))]
    public string Role { get; set; } = "Member";
    public string BloodGroup { get; set; }
    public string Name { get; set; }
}