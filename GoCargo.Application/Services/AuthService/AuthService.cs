using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces.RepositroryInterfaces;
using Application.Interfaces.ServiceInterfaces;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;


        public AuthService(IAuthRepository repository, IConfiguration config, IMapper mapper, ILogger<AuthService> logger)
        {
            _repository = repository;
            _config = config;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            // Trim inputs
            dto.Email = dto.Email?.Trim();
            if (dto.Password != dto.ConfirmPassword)
                throw new ArgumentException("Passwords do not match.");

            if (await _repository.ExistsByEmailAsync(dto.Email))
                throw new InvalidOperationException($"An account with email '{dto.Email}' already exists.");
            if (await _repository.ExistsByphone(dto.PhoneNumber))
                throw new InvalidOperationException($"An account with PhoneNumber '{dto.PhoneNumber}' already exists.");

            var user = _mapper.Map<User>(dto);
            user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            await _repository.AddAsync(user);
            return "Registration successful.";
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByEmailAsync(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
                throw new AuthenticationException("Invalid email or password.");
            if (user.IsBlocked == true)
            {
                _logger.LogWarning("user is blocked");
                return new AuthResponseDto { Error = "user is blocked" };
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Role = user.Role,
                Name = user.Name,
                PhoneNumber=user.PhoneNumber,
                Email = user.Email
            };
        }
    }

}
