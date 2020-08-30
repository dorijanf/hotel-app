using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace HotelApp.API.DbContexts.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public HotelRepository(HotelAppContext hotelAppContext,
                               UserManager<User> userManager,
                               IMapper mapper)
        {
            _hotelAppContext = hotelAppContext;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateHotelAsync(RegisterHotelDTO model, HotelStatus status, ClaimsPrincipal currentUser)
        { 
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            if (_userManager.IsInRoleAsync(user, "Registered user").Result)
            {
                await _userManager.AddToRoleAsync(user, "Hotel manager");
            }
            model.Status = status;

            var hotelManager = new HotelUser();

            var hotel = _mapper.Map<Hotel>(model);
            hotelManager.Hotel = hotel;
            hotelManager.User = user;

            _hotelAppContext.HotelUsers.Add(hotelManager);
            hotel.HotelUsers.ToList().Add(hotelManager);
            _hotelAppContext.Hotels.Add(hotel);
            _hotelAppContext.SaveChanges();
        }

        public Hotel GetHotelById(int id)
        {
            return _hotelAppContext.Hotels.FirstOrDefault(x => x.Id == id);
        }

        public Hotel GetHotelByName(string name)
        {
            return _hotelAppContext.Hotels.FirstOrDefault(x => x.Name == name);
        }

        public async Task UpdateHotelAsync(RegisterHotelDTO model, Hotel hotel)
        {
            if(model.StatusId == 0)
            {
                model.StatusId = hotel.StatusId;
            }
            hotel = _mapper.Map(model, hotel);
            _hotelAppContext.Hotels.Update(hotel);
            _hotelAppContext.SaveChanges();
        }
    }
}
