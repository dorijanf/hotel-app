using AutoMapper;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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

        public IEnumerable<Room> GetRooms(RoomParameters roomParameters, int? hotelId)
        {
            var rooms = _hotelAppContext.Set<Room>().AsQueryable();

            if (roomParameters.City != null)
            {
                rooms = _hotelAppContext.Hotels.Where(h => h.City == roomParameters.City)
                                               .SelectMany(r => r.Rooms);
            }

            if (roomParameters.NumberOfBeds > 0)
            {
                rooms = rooms.Where(r => r.NumberOfBeds == roomParameters.NumberOfBeds);
            }

            if (hotelId.HasValue)
            {
                return PagedList<Room>.ToPagedList(rooms
                    .Where(r => r.HotelId == hotelId),
                    roomParameters.PageNumber,
                    roomParameters.PageSize);
            }
            return PagedList<Room>.ToPagedList(rooms,
                roomParameters.PageNumber,
                roomParameters.PageSize);
        }
    }
}
