using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IRoomRepository
    {
        void AddRoom(AddRoomDTO model);
        void DeleteRoom(int roomId);
        void UpdateRoom(int roomId, AddRoomDTO model);
        Room GetRoomById(int roomId);
        IEnumerable<Room> GetRooms(RoomParameters roomParameters, int? hotelId);
    }
}
