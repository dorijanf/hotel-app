using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class RegisterUserDTO
    {
        [Required(ErrorMessage = "UserName is required")]
        [MaxLength(50, ErrorMessage = "UserName can have a maximum of 50 characters")]
        public string UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(10, ErrorMessage = "Password must have a minimum of 10 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
