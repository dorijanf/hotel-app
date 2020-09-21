using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace HotelApp.API.DbContexts.Entities
{
    public class User : IdentityUser, IDeleteable
    {
        public virtual ICollection<HotelUser> HotelUsers { get; set; }
        public bool IsDeleted { get; set; }
    }
}
