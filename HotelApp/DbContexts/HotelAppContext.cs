using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelApp.API.DbContexts.Entities;
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
        public DbSet<Config> Configurations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(e => e.Status)
                    .WithMany(h => h.Hotels)
                    .HasForeignKey(e => e.StatusId);

                entity.HasMany(e => e.Rooms)
                    .WithOne(h => h.Hotel)
                    .HasForeignKey(e => e.HotelId);


                entity.HasMany(e => e.Managers);
            });

            modelBuilder.Entity<HotelStatus>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(e => e.Hotels)
                    .WithOne(s => s.Status)
                    .HasForeignKey(s => s.StatusId);

                entity.HasData(
                    new HotelStatus { Id = (int) HotelStatusTypes.Active, Name = "Active"},
                    new HotelStatus { Id = (int) HotelStatusTypes.Denied, Name = "Denied" },
                    new HotelStatus { Id = (int) HotelStatusTypes.Inactive, Name = "Inactive" },
                    new HotelStatus { Id = (int) HotelStatusTypes.Pending, Name = "Pending" }
                    );
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Note)
                    .HasMaxLength(250);

                entity.HasOne(e => e.RegisteredUser);
            });

            modelBuilder.Entity<ReservationStatus>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasMany(e => e.Reservations)
                    .WithOne(r => r.ReservationStatus)
                    .HasForeignKey(e => e.ReservationStatusId);

                entity.HasData(
                        new ReservationStatus { Id = (int) ReservationStatusTypes.Accepted, Name = "Accepted" },
                        new ReservationStatus { Id = (int) ReservationStatusTypes.Denied, Name = "Denied" },
                        new ReservationStatus { Id = (int) ReservationStatusTypes.Processing, Name = "Processing" },
                        new ReservationStatus { Id = (int) ReservationStatusTypes.Cancelled, Name = "Cancelled" }
                    );
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.NumberOfBeds)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(e => e.Hotel)
                    .WithMany(r => r.Rooms)
                    .HasForeignKey(r => r.HotelId);

                entity.HasMany(e => e.Reservations);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasData(
                    new UserRole { Id = "SuperAdministrator", Name = "SuperAdministrator", NormalizedName = "superadministrator" },
                    new UserRole { Id = "Administrator", Name = "Administrator", NormalizedName = "administrator" },
                    new UserRole { Id = "Hotel manager", Name = "Hotel manager", NormalizedName = "hotel manager" },
                    new UserRole { Id = "Registered user", Name = "Registered user", NormalizedName = "registered user" }
                    );
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
