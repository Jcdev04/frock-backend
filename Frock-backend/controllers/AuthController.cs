using Frock_backend.access_and_identity.Application.Interfaces;
using Frock_backend.access_and_identity.Domain.Entities;
using Frock_backend.access_and_identity.Domain.ValueObjects;
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
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.RegisterAsync(
                    request.FirstName,
                    request.LastName,
                    request.Email,
                    request.Password,
                    request.Role);

                return CreatedAtAction(nameof(GetUser), new { id = user.Id.Value }, user.ToSafeObject());
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
        public async Task<ActionResult> Login([FromQuery] string email, [FromQuery] string password)
        {
            try
            {
                var user = await _userService.LoginAsync(email, password);

                if (user == null)
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

                return Ok(user.ToSafeObject());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult> GetUser(string id)
        {
            return Ok(new { message = "User created successfully", id });
        }
    }

    // ✅ Clase simple para recibir datos del registro
    public class RegisterRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}