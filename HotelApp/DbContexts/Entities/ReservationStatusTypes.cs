using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public enum ReservationStatusTypes
    {
        Processing = 1,
        Accepted = 2,
        Denied = 3,
        Cancelled = 4
    }
}
