using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class ReservationStatus : IDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Reservation> Reservations { get; set; }

        public bool IsDeleted { get; set; }
    }
}
