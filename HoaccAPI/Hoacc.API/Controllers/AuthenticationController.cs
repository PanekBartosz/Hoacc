using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HoaccAPI.BindingModels;
using HoaccDataSql;

[ApiController]
[Route("api/auth")]
public class AuthenticationController : ControllerBase
{
    private readonly HoaccDbContext _context;

    public AuthenticationController(HoaccDbContext context)
    {
        _context = context;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Invalid request");
        }

        var user = await _context.User
            .FirstOrDefaultAsync(u => u.Email == model.Email && u.Password == model.Password);

        if (user == null)
        {
            return Unauthorized("Invalid email or password");
        }
        return Ok(new { user.UserId});
    }
}