using HotelApp.API.DbContexts.Entities;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace HotelApp.API.Controllers
{
    [Route("api/rooms/{roomId}/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationsController(IReservationRepository reservationRepository,
                                     IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        public async Task<IActionResult> AddReservation([FromRoute] int roomId, [FromBody] ReservationDTO model)
        {
            if(_roomRepository.GetRoomById(roomId) == null)
            {
                return StatusCode(StatusCodes.Status404NotFound,
                                    new ResponseDTO
                                    {
                                        Status = "Not Found",
                                        Message = "Room does not exist."
                                    });
            }

            var reservationId = await _reservationRepository.CreateReservationAsync(model, roomId);

            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Reservation successfully created.",
                EntityId = reservationId
            });
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager, Registered user")]
        [Route("{id}")]
        public IActionResult UpdateReservationStatus(int id, [FromBody] int statusId)
        {
            _reservationRepository.UpdateReservationStatus(id, statusId);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Reservation status updated successfully!"
            });
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        [Route("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _reservationRepository.GetReservationById(id);
            return Ok(reservation);
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        [Route("/api/[controller]")]
        public IActionResult GetAllReservations([FromQuery] ReservationParameters reservationParameters)
        {
            var reservations = _reservationRepository.GetAllReservations(reservationParameters);
            return Ok(reservations);
        }

        [HttpGet]
        [Route("/api/[controller]/count")]
        public int GetReservationsCount([FromQuery] ReservationParameters reservationParameters)
        {
            var reservationCount = _reservationRepository.GetAllReservationsCount(reservationParameters);
            return reservationCount;
        }

        [HttpGet]
        [Route("/api/user-reservations")]
        [Authorize(Roles = "Registered user")]
        public IActionResult GetAllUserReservations([FromQuery] ReservationParameters reservationParameters)
        {
            var reservationCount = _reservationRepository.GetAllUserReservations(reservationParameters);
            return Ok(reservationCount.Result);
        }

        [HttpGet]
        [Route("/api/user-reservations/count")]
        [Authorize(Roles = "Registered user")]
        public IActionResult GetAllUserReservationsCount ([FromQuery] ReservationParameters reservationParameters)
        {
            var reservationCount = _reservationRepository.GetAllUserReservationsCount(reservationParameters);
            return Ok(reservationCount.Result);
        }
    }
}
