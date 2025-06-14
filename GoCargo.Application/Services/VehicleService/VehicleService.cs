﻿using System;
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
using GoCargo.Application.Dto.VehicleDto;
using GoCargo.Application.Interfaces.RepositroryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Services.VehicleService
{
    public class VehicleService : IVehicleService
    {
        private readonly IRepository<Vehicle> _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IRepository<Vehicle> repository, IConfiguration configuration, IMapper mapper, ICloudinaryService cloudinaryService,IVehicleRepository vehicleRepository)
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
            _cloudinaryService = cloudinaryService;
            _vehicleRepository = vehicleRepository;

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

    }
}
