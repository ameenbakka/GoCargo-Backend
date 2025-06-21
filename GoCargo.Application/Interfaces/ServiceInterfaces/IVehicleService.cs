using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.VehicleDto;
using Domain.Models;
using GoCargo.Application.Dto.VehicleDto;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IVehicleService
    {
        Task AddVehicleAsync(int userId, CreateVehicleDto dto, IFormFile image);
        Task<Vehicle> GetAllVehiclesAsync(int id);
        Task UpdateVehicleAsync(int id, CreateVehicleDto dto, IFormFile image);
        Task DeleteVehicleAsync(int id);
        Task UpdateAvailability(int userId, UpdateAvailableDto dto);
        Task<Booking> UpdateDeliveryStatusAsync(int bookingId, string status);
        Task<IEnumerable<DriverBookingDetailsDto>> GetBookingsByDriverIdAsync(int driverId);
        Task<IEnumerable<DriverBookingDto>> GetNearbyBookingsAsync(int driverId);
        Task<DriverBookingDetailsDto> AcceptDeliveryAsync(int bookingId, string status, int driverId);
    }

    }