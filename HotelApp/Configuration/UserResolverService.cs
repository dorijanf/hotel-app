using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace HotelApp.API.Configuration
{
    public class UserResolverService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserResolverService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal GetUser()
        {
            return _httpContextAccessor.HttpContext.User;
        }
    }
}
