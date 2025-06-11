using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public string? VehicleNumber { get; set; }
        public string? VehicleType { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Capacity { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string? Image { get; set; }
        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual User? User { get; set; }
    }
}
