using System.Security.Claims;
using Application.ApiResponse;
using Application.Dto;
using Application.Dto.VehicleDto;
using Application.Interfaces.ServiceInterfaces;
using Application.Services.ProfileService;
using Domain.Models;
using GoCargo.Application.Dto.VehicleDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoCargo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        [HttpGet("Details{id}/Driver")]
        [Authorize(Roles ="Driver")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var vehicle = await _vehicleService.GetAllVehiclesAsync(id);
                return Ok(new ApiResponse<Vehicle>(vehicle, "Vehicle fetched successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default,"No vehicle available" , false)
                {

                });
            }

        }
        // Add new vehicle
        [HttpPost("Create/Driver")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> AddVehicle([FromForm] CreateVehicleDto dto,     IFormFile image)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _vehicleService.AddVehicleAsync(userId, dto, image);
                return Ok(new ApiResponse<CreateVehicleDto>(dto,"Vehicle added successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        // Update vehicle
        [HttpPut("Update{id}/Driver")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> UpdateVehicle( int id, [FromForm]  CreateVehicleDto dto, IFormFile image)
        {
            try
            {
                await _vehicleService.UpdateVehicleAsync(id, dto, image);
                return Ok(new ApiResponse<CreateVehicleDto>(dto,"Vehicle updated successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        // Delete vehicle
        [HttpDelete("Delete{id}/Driver")]
        [Authorize(Roles = "Driver,Admin")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                await _vehicleService.DeleteVehicleAsync(id);
                return Ok(new ApiResponse<string>(null,"Vehicle deleted successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
        // Update vehicle availability
        [HttpPut("Update Availability/Driver")]
        [Authorize(Roles = "Driver")]
        public async Task<IActionResult> UpdateVehicle(UpdateAvailableDto dto)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                await _vehicleService.UpdateAvailability(userId, dto);
                return Ok(new ApiResponse<string>(null, "Availability updated successfully", true));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(default, ex.Message, false));
            }
        }
    }
}
