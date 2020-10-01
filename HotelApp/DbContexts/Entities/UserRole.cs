using Microsoft.AspNetCore.Identity;

namespace HotelApp.API.DbContexts.Entities
{
    public class UserRole : IdentityRole, IDeleteable
    {
        public bool IsDeleted { get; set; }
    }
}
