using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoCargo.Application.Dto.BookingDto
{
    public class CreateBookingDto
    {
        [Required(ErrorMessage ="Enter the PickupLocation")]
        public string? PickupLocation { get; set; }
        [Required(ErrorMessage = "Enter the DropLocation")]
        public string? DropLocation { get; set; }
        public string? GoodsType { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal Weight { get; set; }
        public float DistanceInKm { get; set; }
    }
}
