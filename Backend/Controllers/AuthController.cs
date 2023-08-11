using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Data;
using Backend.Dtos;
using Backend.Enums;
using Backend.Extensions;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly Context _context;
    private readonly Models.SecurityToken _securityToken = new();

    public AuthController(Context context, Models.SecurityToken securityToken)
    {
        _context = context;
        _securityToken = securityToken;
    }

    [HttpPost("[action]")]
    public IActionResult Register(RegisterDto registerDto)
    {
        var isParsed = registerDto.BloodGroup.TryToBloodGroup(out BloodGroup bloodGroup);
        if (!isParsed)
        {
            return BadRequest("Invalid 'bloodGroup' value.");
        }

        var hashedPassword = HashPassword(registerDto.Password);
        var user = new User()
        {
            Email = registerDto.Email,
            PasswordHash = hashedPassword,
            Role = registerDto.Role.ParseEnum<UserRole>(),
            Name = registerDto.Name,
            NormalizedName = StringExtensions.Normalize(registerDto.Name),
            BloodGroup = bloodGroup,
            Mobile = registerDto.Mobile,
            Address = new()
            {
                StreetAddress = registerDto.Address.StreetAddress,
                City = registerDto.Address.City,
                State = registerDto.Address.State,
                Country = registerDto.Address.Country
            }
        };

        _context.Add(user);
        _context.SaveChanges();
        return Ok(new { token = GetJwtToken(user) });
    }

    [HttpPost("[action]")]
    public IActionResult Login(LoginDto loginDto)
    {
        var dbUser = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email);
        if (dbUser == null)
        {
            return BadRequest("Email not found.");
        }

        if (!VerifyHashedPassword(dbUser.PasswordHash, loginDto.Password))
        {
            return BadRequest("Incorrect password.");
        }

        return Ok(new { token = GetJwtToken(dbUser) });
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    private bool VerifyHashedPassword(string hashedPassword, string providedPassword)
    {
        using var sha256 = SHA256.Create();
        var providedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(providedPassword));
        var providedHashedPassword = Convert.ToBase64String(providedBytes);
        return providedHashedPassword == hashedPassword;
    }

    private string GetJwtToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("userId", user.ID.ToString()),
            new Claim("email", user.Email),
            new Claim("name", user.Name),
            new Claim("role", user.RoleStr)
        };

        var expires = DateTime.Now.AddDays(_securityToken.ExpireInDays);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_securityToken.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            _securityToken.Issuer,
            _securityToken.Audience,
            claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}