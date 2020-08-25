using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Note { get; set; }

        public virtual User RegisteredUser { get; set; }
        public virtual ReservationStatus ReservationStatus { get; set;  }
    }
}
