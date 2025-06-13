using Application.ApiResponse;
using Application.Dto.VehicleDto;
using System.Security.Claims;
using GoCargo.Application.Interfaces.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using GoCargo.Application.Dto.BookingDto;
using Domain.Models;

namespace GoCargo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        // Add new Booking
        [HttpPost("Create/User")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> AddBooking([FromBody] CreateBookingDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _bookingService.AddBookingAsync(userId, dto);
                return Ok(new ApiResponse<CreateBookingDto>(dto, "Booking added successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        // Update Booking
        [HttpPut("Update{id}/User")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] CreateBookingDto dto)
        {
            try
            {
                await _bookingService.UpdateBookingAsync(id, dto);
                return Ok(new ApiResponse<CreateBookingDto>(dto, "Booking updated successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        // Delete Booking
        [HttpDelete("Delete{id}/User")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            try
            {
                await _bookingService.DeleteBookingAsync(id);
                return Ok(new ApiResponse<string>(null, "Booking deleted successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        [HttpGet("Bookings/Driver")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllByUserId()
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var bookings = await _bookingService.GetAllBookingAsync(userId);
                return Ok(new ApiResponse<IEnumerable<Booking>>(bookings, "Booking fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, "No Booking available", false)
                {

                });
            }

        }




    }
}
