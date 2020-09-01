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
    [Route("api/[controller]")]
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
            _roomRepository.AddRoom(model);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel room added successfully!"
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
        public IActionResult GetRooms([FromRoute] int? hotelId, [FromQuery] RoomParameters roomParameters)
        {
            var rooms = _roomRepository.GetRooms(roomParameters, hotelId);
            if (rooms.Count() < 1)
            {
                return StatusCode(StatusCodes.Status400BadRequest,
                                    new ResponseDTO
                                    {
                                        Status = "Error",
                                        Message = "There aren't any rooms that satisfy your query."
                                    });
            }
            return Ok(rooms);
        }
    }
}
