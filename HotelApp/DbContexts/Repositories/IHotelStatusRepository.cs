using HotelApp.API.DbContexts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IHotelStatusRepository
    {
        public HotelStatus GetHotelStatusById(int id);
    }
}
