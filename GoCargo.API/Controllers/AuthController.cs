using Application.ApiResponse;
using Application.Dto;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoCargo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto);
                return Ok(new ApiResponse<string>(result, "User registered successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _authService.LoginAsync(dto);
                return Ok(new ApiResponse<AuthResponseDto>(result, "Login successful", true));
            }
            catch (Exception ex)
            {
                return Unauthorized(new ApiResponse<AuthResponseDto>(default, ex.Message, false));
            }
        }
    }
}

