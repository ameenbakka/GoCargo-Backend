using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces.RepositroryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.Extensions.Configuration;

namespace Application.Services.ProfileService
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public ProfileService(IProfileRepository repository, IConfiguration config, IMapper mapper)
        {
            _repository = repository;
            _config = config;
            _mapper = mapper;
        }
        public async Task<GetUserDto> GetProfileAsync(int userId)
        {
            var user = await _repository.GetProfileAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            return new GetUserDto
            {
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber
            };
        }
        public async Task<UpdateUserDto> UpdateProfileAsync(int userId, UpdateUserDto dto)
        {
            var user = await _repository.UpdateProfileAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            user.Name = dto.Name;
            user.PhoneNumber = dto.PhoneNumber;
            await _repository.UpdateAsync(user);
            return new UpdateUserDto
            {
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

        }
    }
}
