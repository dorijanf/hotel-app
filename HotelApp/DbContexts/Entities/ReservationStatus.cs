using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class ReservationStatus
    {
        public int Id { get; set; }
        public ReservationStatusTypes Name { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
