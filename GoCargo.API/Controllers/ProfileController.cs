using System.Security.Claims;
using Application.ApiResponse;
using Application.Dto;
using Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GoCargo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProfileController(IProfileService profileService, IHttpContextAccessor httpContextAccessor)
        {
            _profileService = profileService;
            _httpContextAccessor = httpContextAccessor;
        }
        private string GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        [HttpGet("me")]
        [Authorize(Roles ="User,Driver")]
        public async Task<IActionResult> GetProfile()
        {
            try
            {
                int userId = int.Parse(GetUserId());
                var profile = await _profileService.GetProfileAsync(userId);
                return Ok(new ApiResponse<GetUserDto>(profile, "User profile fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        [HttpPut("Update")]
        [Authorize(Roles = "User,Driver")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            try
            {
                int userId = int.Parse(GetUserId());
                var profile = await _profileService.UpdateProfileAsync(userId, dto);
                return Ok(new ApiResponse<UpdateUserDto>(profile, "User profile updated successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }

    }
}
