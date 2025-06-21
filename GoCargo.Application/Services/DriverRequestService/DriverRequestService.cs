using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.RepositroryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using Application.Services.CloudinaryService;
using Domain.Models;
using GoCargo.Application.Dto.DriverRequestDto;
using GoCargo.Application.Interfaces.ServiceInterfaces;
using GoCargo.Domain.Models;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;

namespace GoCargo.Application.Services.DriverRequestService
{
    public class DriverRequestService : IDriverRequestService
    {
        private readonly IRepository<DriverRequest> _reqRepo;
        private readonly IRepository<User> _userRepo;
        private readonly ICloudinaryService _cloudinaryService;


        public DriverRequestService(IRepository<DriverRequest> reqRepo,IRepository<User> userRepo, ICloudinaryService cloudinaryService)
        {
            _reqRepo = reqRepo;
            _userRepo = userRepo;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<DriverRequest> CreateAsync(int userId, CreateDriverRequestDto dto, IFormFile vehicleimage, IFormFile licenceimage, IFormFile rcimage)
        {
            string VehicleImage = await _cloudinaryService.UploadImage(vehicleimage);
            string LicenceImage = await _cloudinaryService.UploadImage(licenceimage);
            string RcImage = await _cloudinaryService.UploadImage(rcimage);

            var entity = new DriverRequest
            {
                UserId = userId,
                Name = dto.Name,
                PhoneNumber = dto.PhoneNumber,
                VehicleNumber = dto.VehicleNumber,
                VehicleType = dto.VehicleType,
                Capacity = dto.Capacity,
                Location = dto.Location,

                LicenceImage = LicenceImage,
                VehicleImage = VehicleImage,
                RcImage = RcImage
            };

            await _reqRepo.AddAsync(entity);
            return entity;
        }

        public async Task ApproveAsync(int requestId)
        {
            var req = await _reqRepo.GetByIdAsync(requestId)
                      ?? throw new Exception("Request not found");

            req.Status = "Approved";
            await _reqRepo.UpdateAsync(req);

            // promote the user to Driver role
            var user = await _userRepo.GetByIdAsync(req.UserId);
            if (user != null)
            {
                user.Role = "Driver";
                await _userRepo.UpdateAsync(user);
            }
        }

        public async Task RejectAsync(int requestId, string note = null)
        {
            var req = await _reqRepo.GetByIdAsync(requestId)
                      ?? throw new Exception("Request not found");

            req.Status = "Rejected";
            await _reqRepo.UpdateAsync(req);

            // optionally log 'note'
        }
        public async Task<IEnumerable<DriverRequestDto>> GetAllDriverRequestsAsync()
        {
            var requests = await _reqRepo.GetAllAsync();

            return requests.Select(r => new DriverRequestDto
            {
                RequestId = r.Id,
                UserId = r.UserId,
                Name = r.User?.Name,
                PhoneNumber = r.User?.PhoneNumber,
                VehicleNumber = r.VehicleNumber,
                VehicleType = r.VehicleType,
                Location = r.Location,
                LicenceImage = r.LicenceImage,
                VehicleImage = r.VehicleImage,
                RcImage = r.RcImage,
                Status = r.Status
            }).ToList();
        }

    }

}
