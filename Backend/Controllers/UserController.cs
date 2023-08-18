using Backend.Data;
using Backend.Enums;
using Backend.Extensions;
using Backend.Helpers;
using Backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly Context _context;

    public UserController(Context context)
    {
        _context = context;
    }

    [HttpGet("[action]")]
    public IActionResult GetStudentIdAndNames(string searchText)
    {
        searchText = StringExtensions.Normalize(searchText);
        var userIdAndNames = _context.Users.Where(x => x.NormalizedName.Contains(searchText))
            .OrderBy(x => x.Name)
            .Take(5)
            .Select(x => new { x.ID, x.Name });

        return Ok(userIdAndNames);
    }

    [HttpPost("[action]")]
    public IActionResult GetUsers(UserParameters userParameters)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrWhiteSpace(userParameters.BloodGroup))
        {
            query = query.Where(x => x.BloodGroupStr == userParameters.BloodGroup);
        }

        var users = query.OrderBy(u => u.Name)
            .Skip((userParameters.PageNumber - 1) * (userParameters.PageSize))
            .Take(userParameters.PageSize);

        return Ok(users);
    }
}