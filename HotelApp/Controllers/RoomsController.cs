using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace HotelApp.API.Controllers
{
    [Route("api/{HotelId}/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        private readonly ICityRepository _cityRepository;
        public RoomsController(IRoomRepository roomRepository, 
                               ICityRepository cityRepository)
        {
            _roomRepository = roomRepository;
            _cityRepository = cityRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Hotel manager")]
        public IActionResult AddRoom([FromBody] AddRoomDTO model)
        {   // this method creates a room by providing a string and 3 ints (name, number of beds, price and hotel id)
            var roomId = _roomRepository.CreateRoom(model);
            return Ok(roomId);
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
        [Route("{roomId}")]
        public IActionResult UpdateRoom(int roomId, [FromBody] AddRoomDTO model)
        {
            // this method updates a room by providing the rooms id and a model containg all neccessary room parameters
            _roomRepository.UpdateRoom(roomId, model);
            return Ok("Hotel room successfully updated!");
        }

        [HttpDelete]
        [Authorize(Roles = "Hotel manager")]
        [Route("{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            // this method deletes a room by providing the rooms id
            _roomRepository.DeleteRoom(roomId);
            return Ok("Hotel room successfully deleted!");
        }

        [HttpGet]
        [Route("/api/[controller]/count")]
        public int GetRoomsCount([FromQuery] RoomParameters roomParameters)
        {
            // this method retrieves the count of all rooms in order to support pagination
            var roomCount = _roomRepository.GetAllRoomsCount(roomParameters);
            return roomCount;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]")]
        public IActionResult GetAllRooms([FromQuery] RoomParameters roomParameters)
        {
            // this method retrieves all rooms in the database in order to present them in a list of rooms
            // and supports pagination, filtering and sorting
            var rooms = _roomRepository.GetAllRooms(roomParameters);
            return Ok(rooms);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRoomsForHotel(int hotelId, [FromQuery] RoomParameters roomParameters)
        {
            // this method retrieves all rooms for a specific hotel in order to show the rooms of a single hotel
            var rooms = _roomRepository.GetRoomsForHotel(hotelId, roomParameters);
            return Ok(rooms);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/rooms/{roomId}")]
        public IActionResult GetSingleRoom([FromRoute] int roomId)
        {
            // this method retrieves a single id which is provided in the route for the method
            var room = _roomRepository.GetRoomById(roomId);
            return Ok(room);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/cities")]
        public IActionResult GetCities()
        {
            var cities = _cityRepository.GetCities();
            return Ok(cities);
        }
    }
}
