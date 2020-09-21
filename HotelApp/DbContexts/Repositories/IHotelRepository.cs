using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IHotelRepository
    {
        Task<int> CreateHotelAsync(RegisterHotelDTO model);
        void UpdateHotelAsync(int hotelId, RegisterHotelDTO model);
        void UpdateHotelStatus(int hotelId, int statusId);
        Hotel GetHotelByName(string name);
        Hotel GetHotelById(int id);
        public ICollection<Hotel> GetAllHotelsWithSameName(string name);
        public IEnumerable<Hotel> GetAllHotels();
    }
}
