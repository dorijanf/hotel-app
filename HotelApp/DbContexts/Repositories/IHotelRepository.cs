using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IHotelRepository
    {
        Task CreateHotelAsync(RegisterHotelDTO model, HotelStatus status, ClaimsPrincipal currentUser);
        Task UpdateHotelAsync(RegisterHotelDTO model, Hotel hotel);
        Hotel GetHotelByName(string name);
        Hotel GetHotelById(int id);
    }
}
