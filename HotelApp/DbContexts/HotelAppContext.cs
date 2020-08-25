using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts
{
    public class HotelAppContext : IdentityDbContext<User, UserRole, string>
    {

        public HotelAppContext(DbContextOptions<HotelAppContext> options)
            :base(options)
        {
            
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<HotelStatus> HotelStatuses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
    }
}
