using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public enum ReservationStatusType
    {
        Processing,
        Accepted,
        Denied,
        Canceled
    }
    public class ReservationStatus
    {
        public int Id { get; set; }
        public ReservationStatusType Name { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
