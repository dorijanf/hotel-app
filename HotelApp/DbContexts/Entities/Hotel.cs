using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class Hotel
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
