using Frock_backend.access_and_identity.Application.DTOs;
using Frock_backend.access_and_identity.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Frock_backend.controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterUserDto registerDto)
        {
            try
            {
                var user = await _userService.RegisterAsync(registerDto);
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<UserResponseDto>> Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var loginDto = new LoginUserDto { Email = email, Password = password };
                var user = await _userService.LoginAsync(loginDto);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                return Ok(user);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserResponseDto>> GetUser(string id)
        {
            return Ok(new { message = "User created successfully", id });
        }
    }
}