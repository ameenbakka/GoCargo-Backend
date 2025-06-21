using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoCargo.Application.Dto.DriverRequestDto;
using GoCargo.Domain.Models;
using Microsoft.AspNetCore.Http;


namespace GoCargo.Application.Interfaces.ServiceInterfaces
{
    public interface IDriverRequestService
    {
        Task<DriverRequest> CreateAsync(int userId, CreateDriverRequestDto dto, IFormFile vehicleimage, IFormFile licenceimage, IFormFile rcimage);
        Task ApproveAsync(int requestId);
        Task RejectAsync(int requestId, string note = null);
        Task<IEnumerable<DriverRequestDto>> GetAllDriverRequestsAsync();
    }
}
