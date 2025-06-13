using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.VehicleDto;
using Domain.Models;
using GoCargo.Application.Dto.BookingDto;
using Microsoft.AspNetCore.Http;

namespace GoCargo.Application.Interfaces.ServiceInterfaces
{
    public interface IBookingService
    {
        Task AddBookingAsync(int userId, CreateBookingDto dto);
        Task<IEnumerable<Booking>> GetAllBookingAsync(int userId);
        Task UpdateBookingAsync(int id, CreateBookingDto dto);
        Task DeleteBookingAsync(int id);

    }
}
