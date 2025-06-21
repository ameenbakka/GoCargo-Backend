using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCargo.Application.Dto.DriverRequestDto
{
    public class DriverRequestDto
    {
            public int RequestId { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }

            public string VehicleNumber { get; set; }
            public string VehicleType { get; set; }
            public string Capacity { get; set; }
            public string Location { get; set; }

            public string LicenceImage { get; set; }
            public string VehicleImage { get; set; }
            public string RcImage { get; set; }

            public string Status { get; set; }
        }

    }
