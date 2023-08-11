using Backend.Data;
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
        var userIdAndNames = _context.Users.Where(x => x.Name.Contains(searchText))
            .OrderBy(x => x.Name)
            .Take(5)
            .Select(x => new { x.ID, x.Name });

        return Ok(userIdAndNames);
    }
}