using LibraryManagement.Application.DTOs;
using LibraryManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            var result = await _userService.RegisterUserAsync(registerDto);
            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(new { Token = result.Token });
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _userService.AuthenticateUserAsync(loginDto);
            if (!result.IsSuccess)
                return Unauthorized(result.ErrorMessage);

            return Ok(new { Token = result.Token });
        }
    }
}
