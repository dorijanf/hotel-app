using HotelApp.API.DbContexts;
using HotelApp.API.DbContexts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class RegisterHotelDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int StatusId { get; set; }

        public virtual HotelStatus Status { get; set; }
        public virtual ICollection<User> Managers { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }
}
