using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelAppContext _hotelAppContext;

        public HotelRepository(HotelAppContext hotelAppContext)
        {
            _hotelAppContext = hotelAppContext;
        }

        public int Commit()
        {
            return _hotelAppContext.SaveChanges();
        }

        public void CreateHotel(RegisterHotelDTO model, HotelStatus status, User user)
        {
            var hotel = new Hotel
            {
                Name = model.Name,
                Email = model.Email,
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                City = model.City,
                Status = status,
                Managers = new HashSet<User>(),
                Rooms = new HashSet<Room>()
            };

            hotel.Managers.Add(user);
            _hotelAppContext.Hotels.Add(hotel);
            Commit();
        }

        public Hotel GetHotelByName(string name)
        {
            return _hotelAppContext.Hotels.FirstOrDefault(x => x.Name == name);
        }
    }
}
