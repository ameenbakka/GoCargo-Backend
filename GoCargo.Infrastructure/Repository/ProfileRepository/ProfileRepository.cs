using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Application.Interfaces.RepositroryInterfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly AppDbContext _context;

        public ProfileRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetProfileAsync(int userId)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task<User> UpdateProfileAsync(int userId)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.Id == userId);
        }
        public async Task UpdateAsync(User user)
        {
            _context.users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
