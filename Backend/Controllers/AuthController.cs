using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Backend.Data;
using Backend.Dtos;
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
    public IActionResult Register(UserDto userDto)
    {
        var hashedPassword = HashPassword(userDto.Password);
        var user = new User()
        {
            Email = userDto.Email,
            PasswordHash = hashedPassword
        };

        _context.Add(user);
        _context.SaveChanges();
        return Ok(new { token = GetJwtToken(user) });
    }

    [HttpPost("[action]")]
    public IActionResult Login(UserDto userDto)
    {
        var dbUser = _context.Users.FirstOrDefault(x => x.Email == userDto.Email);
        if (dbUser == null)
        {
            return NotFound();
        }

        if (!VerifyHashedPassword(dbUser.PasswordHash, userDto.Password))
        {
            return Unauthorized();
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
            new Claim(JwtRegisteredClaimNames.NameId, user.ID.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
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