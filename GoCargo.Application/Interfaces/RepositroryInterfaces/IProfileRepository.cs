using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Models;

namespace Application.Interfaces.RepositroryInterfaces
{
    public interface IProfileRepository
    {
        Task<User> GetProfileAsync(int userId);
        Task<User> UpdateProfileAsync(int userId);
        Task UpdateAsync(User user);
    }
}
