using System;

namespace HotelApp.API.Models
{
    public class LoginResponseDTO
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
