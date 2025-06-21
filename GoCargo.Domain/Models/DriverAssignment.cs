using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace GoCargo.Domain.Models
{
    public class DriverAssignment
    {
        public int AssignmentId { get; set; }
        public int BookingId { get; set; }

        public int DriverId { get; set; }

        public DateTime AssignedAt { get; set; } = DateTime.Now;

        public string Status { get; set; } = "InProgress";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public virtual Booking? Booking { get; set; }
       public virtual User? User { get; set; }
    }
}
