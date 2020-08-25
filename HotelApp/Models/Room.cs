using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public int Price { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
