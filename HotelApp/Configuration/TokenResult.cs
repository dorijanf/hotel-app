using System;

namespace HotelApp.API.Configuration
{
    public class TokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
    }
}
