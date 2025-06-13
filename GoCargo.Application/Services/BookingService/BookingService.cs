using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Interfaces.RepositroryInterfaces;
using AutoMapper;
using Domain.Models;
using GoCargo.Application.Dto.BookingDto;
using GoCargo.Application.Interfaces.ServiceInterfaces;

namespace GoCargo.Application.Services.BookingService
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _repository;
        private readonly IMapper _mapper;

        public BookingService(IRepository<Booking> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task AddBookingAsync(int userId, CreateBookingDto dto)
        {
            var booking = new Booking
            {
                UserId = userId,
                PickupLocation = dto.PickupLocation,
                DropLocation = dto.DropLocation,
                GoodsType = dto.GoodsType,
                Weight = dto.Weight,
                DistanceInKm = dto.DistanceInKm,
                EstimatedFare = CalculateFare(dto.DistanceInKm),
                BookingDate = DateTime.UtcNow
            };

            await _repository.AddAsync(booking);
        }
        public async Task<IEnumerable<Booking>> GetAllBookingAsync(int userId)
        {
            return await _repository.GetAllByUserIdAsync(userId);
        }

        public async Task UpdateBookingAsync(int id, CreateBookingDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new Exception($"Booking with ID {id} not found.");

            existing.PickupLocation = dto.PickupLocation;
            existing.DropLocation = dto.DropLocation;
            existing.GoodsType = dto.GoodsType;
            existing.Weight = dto.Weight;
            existing.DistanceInKm = dto.DistanceInKm;
            existing.EstimatedFare = CalculateFare(dto.DistanceInKm);
            existing.BookingStatus = "Updated"; // Optional

            await _repository.UpdateAsync(existing);
        }

        public async Task DeleteBookingAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private decimal CalculateFare(float distance)
        {
            return (decimal)distance * 50;
        }
    }
}
