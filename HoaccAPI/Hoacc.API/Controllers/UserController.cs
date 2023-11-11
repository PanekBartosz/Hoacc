using HoaccAPI.Validation;
using HoaccDataSql;
using Microsoft.AspNetCore.Mvc;
using HoaccAPI.Mappers;
using HoaccIServices.User;

namespace HoaccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly HoaccDbContext _context;
        private readonly IUserService _userService;

        public UserController(HoaccDbContext context,IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet("email/{email}", Name = "GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmail(email);
            
            if (user != null)
            {
                return Ok(UserToUserViewModelMapper.UserToUserViewModel(user));
            }

            return NotFound();
        }

        [HttpGet("{userId:min(1)}", Name = "GetUserByUserId")]
        public async Task<IActionResult> GetUserByUserId(int userId)
        {
            var user = await _userService.GetUserByUserId(userId);
            if (user != null)
            {
                return Ok(UserToUserViewModelMapper.UserToUserViewModel(user));
            }
            return NotFound();
        }

        [ValidateModel]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] HoaccIServices.Requests.CreateUser createUser)
        {
            var user = await _userService.CreateUser(createUser);
            return Created(user.UserId.ToString(),UserToUserViewModelMapper.UserToUserViewModel(user)) ;
        }

        [HttpPatch("{userId:min(1)}/password", Name = "UpdateUserPassword")]
        public async Task<IActionResult> UpdateUserPassword(int userId, [FromBody] HoaccIServices.Requests
            .UpdateUserPassword user)
        {
            await _userService.UpdateUserPassword(user, userId);
            return NoContent();
        }

    }
}
