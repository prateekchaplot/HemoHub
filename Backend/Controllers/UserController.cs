using Backend.Data;
using Backend.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
}