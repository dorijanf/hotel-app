using AutoMapper;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;

namespace HotelApp.API.DbContexts.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly IMapper _mapper;
        private readonly ISort<Room> _sort;

        public RoomRepository(HotelAppContext hotelAppContext,
                              IMapper mapper,
                              ISort<Room> sort)
        {
            _hotelAppContext = hotelAppContext;
            _mapper = mapper;
            _sort = sort;
        }

        public int CreateRoom(AddRoomDTO model)
        {
            var room = _mapper.Map<Room>(model);
            _hotelAppContext.Rooms.Add(room);
            _hotelAppContext.SaveChanges();
            return room.Id;
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
            if (room != null)
            {
                _hotelAppContext.Rooms.Remove(room);
                _hotelAppContext.SaveChanges();
            }
        }

        public Room GetRoomById(int id)
        {
            return _hotelAppContext.Rooms.Find(id);
        }

        public IEnumerable<Room> GetRoomsForHotel(int hotelId, RoomParameters roomParameters)
        {
            
            var rooms = _hotelAppContext.Set<Room>().AsQueryable();
            rooms = FilterRooms(ref rooms, roomParameters);
            rooms = rooms.Where(r => r.HotelId == hotelId);
            rooms = _sort.ApplySort(rooms, roomParameters.OrderBy);
            return PagedList<Room>.ToPagedList(rooms,
                roomParameters.PageNumber,
                roomParameters.PageSize);
        }

        public IEnumerable<Room> GetAllRooms(RoomParameters roomParameters)
        {
            var rooms = _hotelAppContext.Set<Room>().AsQueryable();
            rooms = FilterRooms(ref rooms, roomParameters);
            rooms = _sort.ApplySort(rooms, roomParameters.OrderBy);

            return PagedList<Room>.ToPagedList(rooms,
                roomParameters.PageNumber,
                roomParameters.PageSize);
        }

        private IQueryable<Room> FilterRooms(ref IQueryable<Room> rooms, RoomParameters roomParameters)
        {
            if (roomParameters.City != null)
            {
                rooms = _hotelAppContext.Hotels.Where(h => h.City == roomParameters.City)
                                               .SelectMany(r => r.Rooms);
            }

            if (roomParameters.NumberOfBeds > 0)
            {
                rooms = rooms.Where(r => r.NumberOfBeds == roomParameters.NumberOfBeds);
            }

            return rooms;
        }
    }
}
