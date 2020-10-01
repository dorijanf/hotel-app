using AutoMapper;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Extensions.Exceptions;
using HotelApp.API.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace HotelApp.API.DbContexts.Repositories
{
    /*
     * The task of this repository is to manage rooms and it holds methods that create/update and delete rooms
     * but also contains methods that return specific rooms to the user and also filtering methods that support
     * pagination, filtering and sorting capabilities of the api.
     */
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly IMapper _mapper;
        private readonly ISort<Room> _sort;
        private readonly ILogger<RoomRepository> _logger;

        public RoomRepository(HotelAppContext hotelAppContext,
                              IMapper mapper,
                              ISort<Room> sort,
                              ILogger<RoomRepository> logger)
        {
            _hotelAppContext = hotelAppContext;
            _mapper = mapper;
            _sort = sort;
            _logger = logger;
        }

        public int CreateRoom(AddRoomDTO model)
        {
            var room = _mapper.Map<Room>(model);
            _hotelAppContext.Rooms.Add(room);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Room successfully created!");
            return room.Id;
        }

        public void UpdateRoom(int roomId, AddRoomDTO model)
        {
            var room = _mapper.Map<Room>(model);
            room.Id = roomId;
            _hotelAppContext.Rooms.Update(room);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Room successfully updated!");
        }

        public void DeleteRoom(int roomId)
        {
            var room = GetRoomById(roomId);
            _hotelAppContext.Rooms.Remove(room);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Room successfully deleted!");
        }

        public Room GetRoomById(int id)
        {
            var room = _hotelAppContext.Rooms.FirstOrDefault(r => r.Id == id);
            if(room == null)
            {
                throw new NotFoundException($"The room with ID {id} could not be found.");
            }
            return room;
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

        public int GetAllRoomsCount(RoomParameters roomParameters)
        {
            var rooms = _hotelAppContext.Set<Room>().AsQueryable();
            if (roomParameters.HotelId.HasValue)
            {
                roomParameters.PageSize = _hotelAppContext.Rooms.Where(x => x.HotelId == roomParameters.HotelId)
                                                                .Count();
                rooms = _hotelAppContext.Rooms.Where(x => x.HotelId == roomParameters.HotelId);
            }
            else
            {
                roomParameters.PageSize = _hotelAppContext.Rooms.Count();
            }
            rooms = FilterRooms(ref rooms, roomParameters);
            rooms = _sort.ApplySort(rooms, roomParameters.OrderBy);

            return rooms.Count();
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

        private City GetCityById(int id)
        {
            if (_hotelAppContext.Cities.Find(id) == null)
            {
                throw new NotFoundException($"The city with ID {id} does not exist.");
            }
            return _hotelAppContext.Cities.Find(id);
        }

        private IQueryable<Room> FilterRooms(ref IQueryable<Room> rooms, RoomParameters roomParameters)
        {
            if (roomParameters.City.HasValue)
            {
                rooms = _hotelAppContext.Hotels.Where(h => h.CityId == GetCityById((int)roomParameters.City).Id)
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
