using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Application.Dto.VehicleDto;
using Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IVehicleService
    {
            Task AddVehicleAsync(int userId, CreateVehicleDto dto, IFormFile image);
            Task<Vehicle> GetAllVehiclesAsync(int id);
            Task UpdateVehicleAsync(int id , CreateVehicleDto dto, IFormFile image);
            Task DeleteVehicleAsync(int id);
        }

    }