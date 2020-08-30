using HotelApp.API.DbContexts.Entities;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IHotelStatusRepository
    {
        HotelStatus GetHotelStatusById(int id);
    }
}
