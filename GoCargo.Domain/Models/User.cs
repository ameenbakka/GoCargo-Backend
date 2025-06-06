using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class User
    {
       public int Id { get; set; }
       public string? Name { get; set; }
       public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; } = "User";
        public bool IsBlocked { get; set; }
        public string? PhoneNumber { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public virtual List<Booking> Bookings { get; set; }
        public virtual List<Vehicle> Vehicles { get; set; }



    }
}
