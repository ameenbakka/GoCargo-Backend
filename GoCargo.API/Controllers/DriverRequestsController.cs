using Application.ApiResponse;
using GoCargo.Application.Dto.DriverRequestDto;
using GoCargo.Application.Interfaces.ServiceInterfaces;
using GoCargo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Domain.Models;
using GoCargo.Application.Services.AdminService;

namespace GoCargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverRequestsController : ControllerBase
    {
        private readonly IDriverRequestService _service;

        public DriverRequestsController(IDriverRequestService service)
        {
            _service = service;
        }

        // USER submits request
        [HttpPost]
        [Authorize(Roles = "User")]    // ordinary user
        public async Task<IActionResult> CreateRequest([FromForm] CreateDriverRequestDto dto, IFormFile image1, IFormFile image2, IFormFile image3)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _service.CreateAsync(userId, dto,image1,image2,image3);

            return Ok(new ApiResponse<DriverRequest>(result, "Request submitted", true));
        }

        // ADMIN approves
        [HttpPut("{id}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Approve(int id)
        {
            await _service.ApproveAsync(id);
            return Ok(new ApiResponse<string>("OK", "Driver approved", true));
        }

        // ADMIN rejects
        [HttpPut("{id}/reject")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Reject(int id, [FromBody] string note = null)
        {
            await _service.RejectAsync(id, note);
            return Ok(new ApiResponse<string>("OK", "Driver request rejected", true));
        }
        [HttpGet("Driver-Request")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllRequests()
        {
            try
            {
                var users = await _service.GetAllDriverRequestsAsync();
                return Ok(new ApiResponse<IEnumerable<DriverRequestDto>>(users, "Request fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(null, "Error fetching users", false));
            }
        }
    }
}
