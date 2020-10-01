using System.Collections.Generic;

namespace HotelApp.API.DbContexts.Entities
{
    public class HotelStatus : IDeleteable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Hotel> Hotels { get; set; }

        public bool IsDeleted { get; set; }
    }
}
