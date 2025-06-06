using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Dto;
using Domain.Models;

namespace Application.Interfaces.ServiceInterfaces
{
    public interface IProfileService
    {
        Task<GetUserDto> GetProfileAsync(int id);
        Task<UpdateUserDto> UpdateProfileAsync(int userId, UpdateUserDto dto);
    }

}
