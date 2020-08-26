using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class HotelStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
