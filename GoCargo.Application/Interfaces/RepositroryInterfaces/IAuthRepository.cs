using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces.RepositroryInterfaces
{
    public interface IAuthRepository
    {
        Task<bool> ExistsByEmailAsync(string email);
        Task AddAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<bool> ExistsByphone(string Phone);

    }
}
