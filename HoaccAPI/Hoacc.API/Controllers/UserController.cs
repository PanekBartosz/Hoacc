
using Microsoft.AspNetCore.Mvc;

namespace HoaccAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string Greetings()
        {
            return "Hello";
        }
    }
}
