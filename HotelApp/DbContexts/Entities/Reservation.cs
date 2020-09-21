using System;

namespace HotelApp.API.DbContexts.Entities
{
    public class Reservation : IDeleteable
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Note { get; set; }
        public string RegisteredUserId { get; set; }
        public int RoomId { get; set; }
        public int ReservationStatusId { get; set; }

        public virtual User RegisteredUser { get; set; }
        public virtual Room Room { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set;  }

        public bool IsDeleted { get; set; }
    }
}
