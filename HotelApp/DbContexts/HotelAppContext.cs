using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using HotelApp.API.DbContexts.Entities;
using System.Linq;

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
        public DbSet<HotelUser> HotelUsers { get; set; }
        public DbSet<City> Cities { get; set; }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        private void UpdateSoftDeleteStatuses()
        {
            ChangeTracker.DetectChanges();

            var markedAsDeleted = ChangeTracker.Entries().
                Where(x => x.State == EntityState.Deleted);

            foreach(var item in markedAsDeleted)
            {
                if(item.Entity is IDeleteable entity)
                {
                    item.State = EntityState.Unchanged;
                    entity.IsDeleted = true;
                }
            }
        }

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

                entity.Property(e => e.ContactNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CityId)
                    .IsRequired().HasDefaultValue(1);

                entity.HasOne(e => e.Status)
                    .WithMany(h => h.Hotels)
                    .HasForeignKey(e => e.StatusId);

                entity.HasMany(e => e.Rooms)
                    .WithOne(h => h.Hotel)
                    .HasForeignKey(e => e.HotelId);

                entity.HasOne(e => e.City)
                    .WithMany(h => h.Hotels)
                    .HasForeignKey(e => e.CityId);

                entity.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(50);

                entity.HasMany(e => e.Hotels)
                    .WithOne(c => c.City)
                    .HasForeignKey(c => c.CityId);

                entity.HasQueryFilter(p => !p.IsDeleted);

                entity.HasData(
                    new City { Id = 1, Name = "New York", IsDeleted = false },
                    new City { Id = 2, Name = "Paris", IsDeleted = false },
                    new City { Id = 3, Name = "London", IsDeleted = false },
                    new City { Id = 4, Name = "Zagreb", IsDeleted = false },
                    new City { Id = 5, Name = "Budapest", IsDeleted = false },
                    new City { Id = 6, Name = "Rijeka", IsDeleted = false },
                    new City { Id = 7, Name = "Tokyo", IsDeleted = false },
                    new City { Id = 8, Name = "Rio de Janeiro", IsDeleted = false },
                    new City { Id = 9, Name = "Cape Town", IsDeleted = false },
                    new City { Id = 10, Name = "Shanghai", IsDeleted = false },
                    new City { Id = 11, Name = "Madrid", IsDeleted = false },
                    new City { Id = 12, Name = "Rome", IsDeleted = false },
                    new City { Id = 13, Name = "Barcelona", IsDeleted = false }
                 );
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

                entity.HasOne(r => r.Room)
                    .WithMany(e => e.Reservations)
                    .HasForeignKey(e => e.RoomId);

                entity.HasQueryFilter(p => !p.IsDeleted);

            });

            modelBuilder.Entity<HotelUser>(entity =>
            {
                entity.HasKey(hu => new
                {
                    hu.HotelId,
                    hu.UserId
                });

                entity.HasQueryFilter(p => !p.IsDeleted);
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

                entity.HasQueryFilter(p => !p.IsDeleted);
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

                entity.HasMany(e => e.Reservations)
                    .WithOne(r => r.Room)
                    .HasForeignKey(e => e.RoomId);

                entity.HasQueryFilter(p => !p.IsDeleted);
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

                entity.HasQueryFilter(p => !p.IsDeleted);
            });
        }
    }
}
