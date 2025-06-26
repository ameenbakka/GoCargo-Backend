using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.VehicleDto;
using Application.Interfaces.RepositroryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Application.Services.CloudinaryService;
using AutoMapper;
using Domain.Models;
using GoCargo.Application.Dto.BookingDto;
using GoCargo.Application.Dto.VehicleDto;
using GoCargo.Application.Interfaces.RepositroryInterfaces;
using GoCargo.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IRepository<Booking> _bookingrepo;
        private readonly IRepository<DriverAssignment> _assirepo;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IRepository<Vehicle> repository, IConfiguration configuration, IMapper mapper, ICloudinaryService cloudinaryService,IVehicleRepository vehicleRepository, IRepository<Booking> bookingrepo, IRepository<DriverAssignment> assirepo)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _vehicleRepository = vehicleRepository;
            _assirepo = assirepo;
            _bookingrepo = bookingrepo;

        }
        public async Task AddVehicleAsync(int userId, CreateVehicleDto dto, IFormFile image)
        {
            string imageUrl = await _cloudinaryService.UploadImage(image);
            var vehicle = new Vehicle
            {
                DriverId = userId,
                VehicleNumber = dto.VehicleNumber,
                VehicleType = dto.VehicleType,
                Capacity = dto.Capacity,
                Location = dto.Location,
                Image = imageUrl
            };

            await _repository.AddAsync(vehicle);
        }

        public async Task<Vehicle> GetAllVehiclesAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task UpdateVehicleAsync(int id, CreateVehicleDto dto, IFormFile image)
        {
            var existingVehicle = await _repository.GetByIdAsync(id);
            if (existingVehicle == null)
                throw new Exception($"Vehicle with ID {id} not found");
            string imageUrl = await _cloudinaryService.UploadImage(image);
            existingVehicle.VehicleNumber = dto.VehicleNumber;
            existingVehicle.VehicleType = dto.VehicleType;
            existingVehicle.Capacity = dto.Capacity;
            existingVehicle.Image = imageUrl;
            existingVehicle.Location = dto.Location;

            await _repository.UpdateAsync(existingVehicle);
        }

        public async Task DeleteVehicleAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
        public async Task UpdateAvailability(int userId, UpdateAvailableDto dto)
        {
            var existingVehicle = await _vehicleRepository.GetVehicleAsync(userId);
            existingVehicle.IsAvailable = dto.IsAvailable;
            await _repository.UpdateAsync(existingVehicle);
        }
        public async Task<BookingStatusDto> UpdateDeliveryStatusAsync(int bookingId, string status)
        {
            // Step 1: Get booking
            var booking = await _bookingrepo.GetByIdAsync(bookingId);
            if (booking == null)
                throw new Exception("Booking not found");

            // Step 2: Update booking status
            booking.BookingStatus = status;
            await _bookingrepo.UpdateAsync(booking);

            // Step 3: Update corresponding DriverAssignment status
            var assignment = await _assirepo
                .GetAllAsync(); // Assuming GetAllAsync is available

            var assignmentToUpdate = assignment.FirstOrDefault(a => a.BookingId == bookingId);
            if (assignmentToUpdate != null)
            {
                assignmentToUpdate.Status = status;
                await _assirepo.UpdateAsync(assignmentToUpdate);
            }

            return new BookingStatusDto
            {
                BookingId = booking.BookingId,
                Status= booking.BookingStatus
            };
        }
        public async Task<IEnumerable<DriverBookingDetailsDto>> GetBookingsByDriverIdAsync(int driverId)
        {
            var assignments = await _assirepo.GetAllAsync();
            var assignedBookingIds = assignments
                .Where(a => a.DriverId == driverId)
                .Select(a => a.BookingId)
                .ToHashSet();

            var bookings = await _vehicleRepository.GetAllWithUserAsync();

            var detailedBookings = bookings
                .Where(b => assignedBookingIds.Contains(b.BookingId))
                .Select(b => new DriverBookingDetailsDto
                {
                    BookingId = b.BookingId,
                    PickupLocation = b.PickupLocation,
                    DropLocation = b.DropLocation,
                    BookingDate = b.BookingDate,
                    Status = b.BookingStatus,
                    GoodsType = b.GoodsType,
                    Weight = b.Weight,
                    DistanceInKm = b.DistanceInKm,
                    EstimatedFare = b.EstimatedFare,
                    CustomerName = b.User?.Name,           
                    CustomerPhone = b.User?.PhoneNumber     
                });

            return detailedBookings;
        }
        public async Task<IEnumerable<DriverBookingDto>> GetNearbyBookingsAsync(int driverId)
        {
            // Get driver vehicle
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();
            var driverVehicle = vehicles.FirstOrDefault(v => v.DriverId == driverId);
            if (driverVehicle == null) return Enumerable.Empty<DriverBookingDto>();

            // Get bookings
            var bookings = await _vehicleRepository.GetAllWithUserAsync(); // Includes User

            var nearbyBookings = bookings
                .Where(b => b.PickupLocation == driverVehicle.Location)
                .Select(b => new DriverBookingDto
                {
                    BookingId = b.BookingId,
                    PickupLocation = b.PickupLocation,
                    DropLocation = b.DropLocation,
                    BookingDate = b.BookingDate,
                    Status = b.BookingStatus,
                    GoodsType = b.GoodsType,
                    Weight = b.Weight,
                    CustomerName = b.User?.Name,
                    CustomerPhone = b.User?.PhoneNumber
                });

            return nearbyBookings;
        }
        public async Task<DriverBookingDetailsDto> AcceptDeliveryAsync(int bookingId, string status, int driverId)
        {
            // 1. Fetch booking
            var booking = await _bookingrepo.GetByIdAsync(bookingId)
                             ?? throw new Exception("Booking not found");

            // 2. Update booking status
            booking.BookingStatus = status;
            await _bookingrepo.UpdateAsync(booking);

            // 3. Ensure DriverAssignment when status == InProgress
            if (status.Equals("InProgress", StringComparison.OrdinalIgnoreCase))
            {
                var existing = (await _assirepo.FindAsync(
                                    a => a.BookingId == bookingId && a.DriverId == driverId))
                               .FirstOrDefault();

                if (existing == null)
                {
                    await _assirepo.AddAsync(new DriverAssignment
                    {
                        BookingId = bookingId,
                        DriverId = driverId,
                        AssignedAt = DateTime.UtcNow,
                        Status = "InProgress",
                        CreatedAt = DateTime.UtcNow
                    });
                }
                else
                {
                    existing.Status = "InProgress";
                    await _assirepo.UpdateAsync(existing);
                }
            }

            // 4. Return a DTO (no nav‑loops)
            return new DriverBookingDetailsDto
            {
                BookingId = booking.BookingId,
                PickupLocation = booking.PickupLocation,
                DropLocation = booking.DropLocation,
                BookingDate = booking.BookingDate,
                Status = booking.BookingStatus,
                GoodsType = booking.GoodsType,
                Weight = booking.Weight,
                CustomerName = booking.User?.Name,
                CustomerPhone = booking.User?.PhoneNumber
            };
        }






    }
}
