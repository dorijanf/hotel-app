using Microsoft.AspNetCore.Identity;
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
                    new HotelStatus { Id = 1, Name = "Active" },
                    new HotelStatus { Id = 2, Name = "Innactive" },
                    new HotelStatus { Id = 3, Name = "Pending" },
                    new HotelStatus { Id = 4, Name = "Denied" }
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
                        new ReservationStatus { Id = 1, Name = "Processing"},
                        new ReservationStatus { Id = 2, Name = "Accepted" },
                        new ReservationStatus { Id = 3, Name = "Denied" },
                        new ReservationStatus { Id = 4, Name = "Cancelled" }
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
                    new UserRole { Id = "1", Name = "SuperAdministrator", NormalizedName = "superadministrator" },
                    new UserRole { Id = "2", Name = "Administrator", NormalizedName = "administrator" },
                    new UserRole { Id = "3", Name = "Hotel manager", NormalizedName = "hotel manager" },
                    new UserRole { Id = "4", Name = "Registered user", NormalizedName = "registered user" }
                    );
            });

            modelBuilder.Entity<Configuration>(entity =>
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
