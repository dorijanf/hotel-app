using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public enum HotelStatusType
    {
        Unconfirmed,
        Active,
        Inactive,
        Denied
    }

    public class HotelStatus
    {
        public int Id { get; set; }
        public HotelStatusType Name { get; set; }
        
        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
