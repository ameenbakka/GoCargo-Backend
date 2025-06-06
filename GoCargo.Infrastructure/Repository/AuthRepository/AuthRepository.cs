using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.RepositroryInterfaces;
using Domain.Models;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
            private readonly AppDbContext _context;

            public AuthRepository(AppDbContext context)
            {
                _context = context;
            }

            public async Task<bool> ExistsByEmailAsync(string email)
            {
                return await _context.users.AnyAsync(u => u.Email == email);
            }
        public async Task<bool> ExistsByphone(string Phone)
        {
            return await _context.users.AnyAsync(u => u.PhoneNumber == Phone);
        }
        public async Task AddAsync(User user)
            {
                _context.users.Add(user);
                await _context.SaveChangesAsync();
            }

            public async Task<User> GetByEmailAsync(string email)
            {
                return await _context.users.FirstOrDefaultAsync(u => u.Email == email);
            }
    }
}
