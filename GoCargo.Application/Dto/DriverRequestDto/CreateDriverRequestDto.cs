using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCargo.Application.Dto.DriverRequestDto
{
    public class CreateDriverRequestDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        public decimal Capacity { get; set; }
        public string Location { get; set; }
    }
}
