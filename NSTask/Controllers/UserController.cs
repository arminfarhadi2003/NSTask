using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask.Models.Dtos;
using NSTask.Services;

namespace NSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> LogIn(string email, string password)
        {
            var login = await _userService.LogIn(email, password);
            if (login.IsSuccest != false)
            {
                return Ok(login);
            }
            else
            {
                return Unauthorized();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SingUp([FromBody] SingUpDto dto)
        {
            var login = await _userService.SingUp(dto);
            if (login != false)
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
