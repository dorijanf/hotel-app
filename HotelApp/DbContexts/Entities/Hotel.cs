using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HotelApp.API.DbContexts.Entities
{
    public class Hotel : IDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public int StatusId { get; set; }

        public virtual HotelStatus Status { get; set; }
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
        public virtual City City { get; set; }

        public bool IsDeleted { get; set; }
    }
}
