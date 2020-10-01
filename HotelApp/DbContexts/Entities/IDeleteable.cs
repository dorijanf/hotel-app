using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Entities
{
    interface IDeleteable
    {
        bool IsDeleted { get; set; }
    }
}
