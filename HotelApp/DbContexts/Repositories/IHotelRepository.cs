using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IHotelRepository
    {
        public void CreateHotel(RegisterHotelDTO model, HotelStatus status, User user);
        public Hotel GetHotelByName(string name);
        public int Commit();
    }
}
