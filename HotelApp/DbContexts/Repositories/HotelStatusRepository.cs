using HotelApp.API.DbContexts.Entities;
using System.Linq;

namespace HotelApp.API.DbContexts.Repositories
{
    /*
     * This is a repository that contains a single method that retrieves a hotels status by id     
     */
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
