using HotelApp.API.DbContexts;
using HotelApp.API.DbContexts.Entities;
using System;
using System.Collections.Generic;

namespace HotelApp.API.Models
{
    public class RegisterHotelDTO
    {
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int? StatusId { get; set; }
    }
}
