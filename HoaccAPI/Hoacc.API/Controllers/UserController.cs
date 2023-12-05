using HoaccAPI.Mappers;
using HoaccAPI.Validation;
using HoaccDataSql;
using HoaccIServices.Requests;
using HoaccIServices.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoaccAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly HoaccDbContext _context;
    private readonly IUserService _userService;

    public UserController(HoaccDbContext context, IUserService userService)
    {
        _context = context;
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet("email/{email}", Name = "GetUserByEmail")]
    public async Task<IActionResult> GetUserByEmail(string email)
    {
        var user = await _userService.GetUserByEmail(email);

        if (user != null) return Ok(UserToUserViewModelMapper.UserToUserViewModel(user));

        return NotFound();
    }
    
    [Authorize]
    [HttpGet("{userId:min(1)}", Name = "GetUserByUserId")]
    public async Task<IActionResult> GetUserByUserId(int userId)
    {
        var user = await _userService.GetUserByUserId(userId);
        if (user != null) return Ok(UserToUserViewModelMapper.UserToUserViewModel(user));
        return NotFound();
    }
    
    [Authorize]
    [HttpGet("{userId:min(1)}/email", Name = "GetEmailByUserId")]
    public async Task<IActionResult> GetEmailByUserId(int userId)
    {
        var email = await _userService.GetEmailByUserId(userId);
        if (email != null) 
        {
            return Ok(email);
        }
        return NotFound();
    }

    [ValidateModel]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateUser createUser)
    {
        var user = await _userService.CreateUser(createUser);
        return Created(user.UserId.ToString(), UserToUserViewModelMapper.UserToUserViewModel(user));
    }
    
    [Authorize]
    [HttpPatch("{userId:min(1)}/password", Name = "UpdateUserPassword")]
    public async Task<IActionResult> UpdateUserPassword(int userId, [FromBody] UpdateUserPassword user)
    {
        await _userService.UpdateUserPassword(user, userId);
        return NoContent();
    }
}