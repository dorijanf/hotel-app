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
    [Route("api/{hotelId}/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository _roomRepository;
        public RoomsController(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Hotel manager")]
        public IActionResult AddRoom(int hotelId, [FromBody] AddRoomDTO model)
        {   
            model.HotelId = hotelId;
            var roomId = _roomRepository.CreateRoom(model);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel room added successfully!",
                EntityId = roomId
            });
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
        [Route("{roomId}")]
        public IActionResult UpdateRoom(int hotelId, int roomId, [FromBody] AddRoomDTO model)
        {
            model.HotelId = hotelId;
            _roomRepository.UpdateRoom(roomId, model);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel room updated successfully!"
            });
        }

        [HttpDelete]
        [Authorize(Roles = "Hotel manager")]
        [Route("{roomId}")]
        public IActionResult DeleteRoom(int roomId)
        {
            _roomRepository.DeleteRoom(roomId);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel room deleted successfully!"
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/[controller]")]
        public IActionResult GetAllRooms([FromQuery] RoomParameters roomParameters)
        {
            var rooms = _roomRepository.GetAllRooms(roomParameters);
            return Ok(rooms);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetRoomsForHotel(int hotelId, [FromQuery] RoomParameters roomParameters)
        {
            var rooms = _roomRepository.GetRoomsForHotel(hotelId, roomParameters);
            return Ok(rooms);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("/api/rooms/{roomId}")]
        public IActionResult GetSingleRoom([FromRoute] int roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if(room != null) 
            {
                return Ok(room);
            }

            return StatusCode(StatusCodes.Status404NotFound,
                    new ResponseDTO
                    {
                        Status = "Not Found",
                        Message = "Room does not exist."
                    });
        }
    }
}
