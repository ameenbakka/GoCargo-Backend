using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.RepositroryInterfaces;
using AutoMapper;
using Domain.Models;
using GoCargo.Application.Interfaces.ServiceInterfaces;
using GoCargo.Domain.Models;

namespace GoCargo.Application.Services.AdminService
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Booking> _bookingrepo;
        private readonly IRepository<User> _userrepo;
        private readonly IRepository<DriverAssignment> _assrepo;
        private readonly IMapper _mapper;

        public AdminService(IRepository<Booking> bookingrepo, IRepository<User> userrepo, IRepository<DriverAssignment> assrepo, IMapper mapper)
        {
            _assrepo = assrepo;
            _bookingrepo = bookingrepo;
            _userrepo = userrepo;
            _mapper = mapper;
        }
        public Task<IEnumerable<Booking>> GetAllBookingsAsync() => _bookingrepo.GetAllAsync();
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _userrepo.GetAllAsync();
            return users.Where(u => u.Role == "User");
        }
        public async Task<IEnumerable<User>> GetAllDriversAsync()
        {
            var users = await _userrepo.GetAllAsync();
            return users.Where(u => u.Role == "Driver");
        }
        public async Task<DriverAssignment> AssignDriverAsync(int bookingId, int driverId)
        {
            var assignment = new DriverAssignment
            {
                BookingId = bookingId,
                DriverId = driverId,
                AssignedAt = DateTime.Now,
                Status = "InProgress",
                CreatedAt = DateTime.Now
            };
            await _assrepo.AddAsync(assignment);
            var booking = await _bookingrepo.GetByIdAsync(bookingId);
            if (booking != null)
            {
                booking.BookingStatus = "InProgress";
                await _bookingrepo.UpdateAsync(booking);
            }
            return assignment;
        }

    }
}
