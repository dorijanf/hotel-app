using System;
using System.Collections.Generic;

namespace HotelApp.API.Models
{
    public class LoginResponseDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public bool isRegistered { get; set; }
        public bool isManager { get; set; }
        public bool isAdmin { get; set; }
        public bool isSuperAdmin { get; set; }
    }
}
