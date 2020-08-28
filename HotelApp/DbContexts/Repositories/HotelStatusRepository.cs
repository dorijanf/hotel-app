using HotelApp.API.DbContexts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public class HotelStatusRepository : IHotelStatusRepository
    {
        private readonly HotelAppContext _hotelAppContext;

        public HotelStatusRepository(HotelAppContext hotelAppContext)
        {
            _hotelAppContext = hotelAppContext;
        }
        public HotelStatus GetHotelStatusById(int id)
        {
            return _hotelAppContext.HotelStatuses.FirstOrDefault(x => x.Id == id);
        }
    }
}
