using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    public class HotelUser : IDeleteable
    {
        public int HotelId { get; set; }
        public string UserId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual User User { get; set; }

        public bool IsDeleted { get; set; }

    }
}
