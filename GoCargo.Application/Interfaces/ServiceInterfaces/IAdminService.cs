using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using GoCargo.Domain.Models;

namespace GoCargo.Application.Interfaces.ServiceInterfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<Booking>> GetAllBookingsAsync();
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<IEnumerable<User>> GetAllDriversAsync();

        Task<DriverAssignment> AssignDriverAsync(int bookingId, int driverId);
    }
}
