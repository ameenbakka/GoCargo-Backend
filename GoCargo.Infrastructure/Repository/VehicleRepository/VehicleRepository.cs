using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.ServiceInterfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using GoCargo.Application.Interfaces.RepositroryInterfaces;

namespace GoCargo.Infrastructure.Repository.VehicleRepository
{
    public class VehicleRepository: IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Vehicle> GetVehicleAsync(int userId)
        {
            return await _context.vehicles.FirstOrDefaultAsync(u => u.DriverId == userId);
        }
        public async Task UpdateAsync(Vehicle vehicle)
        {
            _context.vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
    }
}
