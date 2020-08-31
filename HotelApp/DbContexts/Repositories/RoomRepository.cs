using AutoMapper;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly IMapper _mapper;

        public RoomRepository(HotelAppContext hotelAppContext,
                              IMapper mapper)
        {
            _hotelAppContext = hotelAppContext;
            _mapper = mapper;
        }

        public void AddRoom(AddRoomDTO model)
        {
            var room = _mapper.Map<Room>(model);
            _hotelAppContext.Rooms.Add(room);
            _hotelAppContext.SaveChanges();
        }

        public void UpdateRoom(int roomId, AddRoomDTO model)
        {
            var room = GetRoomById(roomId);
            room = _mapper.Map<Room>(model);
            _hotelAppContext.Rooms.Update(room);
            _hotelAppContext.SaveChanges();
        }

        public void DeleteRoom(int roomId)
        {
            var room = _hotelAppContext.Rooms.Find(roomId);
            if(room != null) 
            {
                _hotelAppContext.Rooms.Remove(room);
                _hotelAppContext.SaveChanges();
            }
        }

        public Room GetRoomById(int id)
        {
            return _hotelAppContext.Rooms.Find(id);
        }
    }
}
