using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.VehicleDto
{
    public class CreateVehicleDto
    {
        [Required]
        [MaxLength(50)]
        public string VehicleNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string? VehicleType { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Capacity { get; set; }
    }
}
