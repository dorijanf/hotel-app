using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class LoginUserDTO
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
