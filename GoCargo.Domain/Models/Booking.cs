using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        public int BookingId { get; set; }
        public int UserId { get; set; }
        public string? PickupLocation { get; set; }
        public string? DropLocation { get; set; }
        public string? GoodsType { get; set; }
        public decimal Weight { get; set; }
        public float DistanceInKm { get; set; }
        public decimal EstimatedFare { get; set; }
        public string BookingStatus { get; set; } = "Pending";
        public DateTime BookingDate { get; set; } = DateTime.UtcNow;
        public virtual User? User { get; set; }

    }
}
