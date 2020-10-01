using HotelApp.API.DbContexts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly HotelAppContext _hotelAppContext;

        public CityRepository(HotelAppContext hotelAppContext)
        {
            _hotelAppContext = hotelAppContext;
        }

        public IEnumerable<City> GetCities()
        {
            return _hotelAppContext.Cities;
        }
    }
}
