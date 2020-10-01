using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class Room : IDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public int Price { get; set; }
        public int HotelId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public bool IsDeleted { get; set; }
    }
}
