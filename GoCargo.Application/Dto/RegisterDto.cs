using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto
{
    public class RegisterDto
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Phone Number is required.")]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        [MinLength(6, ErrorMessage = "Password must be above 6 Characters")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
   ErrorMessage = "Password must contain at least one letter, one number, and one special character.")]
        public string? Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password do not Match.")]
        public string? ConfirmPassword { get; set; }
    }
}
