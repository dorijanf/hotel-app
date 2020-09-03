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
    [Route("api/{hotelId}/rooms/{roomId}/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IRoomRepository _roomRepository;

        public ReservationController(IReservationRepository reservationRepository,
                                     IRoomRepository roomRepository)
        {
            _reservationRepository = reservationRepository;
            _roomRepository = roomRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        public async Task<IActionResult> AddReservation([FromQuery] string dateFrom,
                                                        [FromQuery] string dateTo,
                                                        [FromRoute] int roomId,
                                                        [FromBody] ReservationDTO model)
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

            if (dateFrom != null)
            {
                model.DateFrom = DateTime.Parse(dateFrom);
            }

            if (dateTo != null)
            {
                model.DateTo = DateTime.Parse(dateTo);
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
        [Authorize(Roles = "Registered user")]
        [Route("{id}/cancel")]
        public IActionResult CancelReservation(int id)
        {
            _reservationRepository.UpdateReservationStatus(id, (int) ReservationStatusTypes.Cancelled);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Reservation successfully cancelled!"
            });
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
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
        [Authorize(Roles = "Hotel manager")]
        public IActionResult GetAllReservations(int roomId, [FromQuery] ReservationParameters reservationParameters)
        {
            var reservations = _reservationRepository.GetAllReservations(roomId, reservationParameters);
            return Ok(reservations);
        }
    }
}
