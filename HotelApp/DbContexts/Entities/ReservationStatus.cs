using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts
{
    public class ReservationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
