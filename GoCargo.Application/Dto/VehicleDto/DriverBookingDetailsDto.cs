using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCargo.Application.Dto.VehicleDto
{
    public class DriverBookingDetailsDto
    {
        public int BookingId { get; set; }
        public string PickupLocation { get; set; }
        public string DropLocation { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; }
        public string? GoodsType { get; set; }
        public decimal Weight { get; set; }
        public float DistanceInKm { get; set; }
        public decimal EstimatedFare { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
    }
}
