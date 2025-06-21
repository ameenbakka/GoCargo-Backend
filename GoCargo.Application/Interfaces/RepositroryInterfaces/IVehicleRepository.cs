using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace GoCargo.Application.Interfaces.RepositroryInterfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleAsync(int userId);
        Task UpdateAsync(Vehicle vehicle);
        Task<IEnumerable<Booking>> GetAllWithUserAsync();
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();

    }
}
