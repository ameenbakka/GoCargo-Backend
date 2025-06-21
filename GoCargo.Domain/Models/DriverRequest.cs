using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace GoCargo.Domain.Models
{
    public class DriverRequest
    {
        public int Id { get; set; }

        public int UserId { get; set; }        // who is requesting
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public string VehicleNumber { get; set; }
        public string VehicleType { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Capacity { get; set; }   // kg/tons

        public string Location { get; set; }

        public string LicenceImage { get; set; }   // URLs or blob paths
        public string VehicleImage { get; set; }
        public string RcImage { get; set; }

        public string Status { get; set; } = "Pending"; // Pending | Approved | Rejected
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual User? User { get; set; }
    }
}
