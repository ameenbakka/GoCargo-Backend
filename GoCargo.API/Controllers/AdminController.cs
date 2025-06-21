using Application.ApiResponse;
using Domain.Models;
using GoCargo.Application.Dto.AssingmentDto;
using GoCargo.Application.Interfaces.ServiceInterfaces;
using GoCargo.Application.Services.BookingService;
using GoCargo.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoCargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [HttpGet("bookings")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllBookings()
        {
            try
            {
                var bookings = await _adminService.GetAllBookingsAsync();
                return Ok(new ApiResponse<IEnumerable<Booking>>(bookings, "Bookings fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(null, "Error fetching bookings", false));
            }
        }

        // GET /api/admin/users
        [HttpGet("Drivers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDrivers()
        {
            try
            {
                var users = await _adminService.GetAllDriversAsync();
                return Ok(new ApiResponse<IEnumerable<User>>(users, "Drivers fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(null, "Error fetching users", false));
            }
        }
        // GET /api/admin/users
        [HttpGet("Users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _adminService.GetAllUsersAsync();
                return Ok(new ApiResponse<IEnumerable<User>>(users, "Users fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(null, "Error fetching users", false));
            }
        }

        // PUT /api/admin/assign-driver
        [HttpPut("assign-driver")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignDriver([FromBody] AssignDriverDto dto)
        {
            try
            {
                var result = await _adminService.AssignDriverAsync(dto.BookingId, dto.DriverId);
                return Ok(new ApiResponse<DriverAssignment>(result, "Driver assigned successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(null, "Failed to assign driver", false));
            }
        }
    }

}
