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
            /*
             * This method requires a room id and the starting and end date of a reservation as well as an optional note
             * that a user can leave to the hotel manager. It assigns a reservation to a specified room. 
             */
            if (_roomRepository.GetRoomById(roomId) == null)
            {
                throw new NullReferenceException("The room that you requested does not exist.");
            }
            var reservationId = await _reservationRepository.CreateReservationAsync(model, roomId);

            return Ok(reservationId);
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager, Registered user")]
        [Route("{id}")]
        public IActionResult UpdateReservationStatus(int id, [FromBody] int statusId)
        {
            // this method is used for a hotel manager to deny or approve a reservation or a registered user
            // to cancel the reservation
            _reservationRepository.UpdateReservationStatus(id, statusId);
            return Ok("Reservation status updated successfully!");
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        [Route("{id}")]
        public IActionResult GetReservation(int id)
        {
            // this method retrieves a reservation by its id
            var reservation = _reservationRepository.GetReservationById(id);
            return Ok(reservation);
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        [Route("/api/[controller]")]
        public IActionResult GetAllReservations([FromQuery] ReservationParameters reservationParameters)
        {
            // this method retrieves a list of all reservations for a specified hotel manager and supports
            // pagination, filtering and sorting
            var reservations = _reservationRepository.GetAllReservations(reservationParameters);
            return Ok(reservations);
        }

        [HttpGet]
        [Route("/api/[controller]/count")]
        public int GetReservationsCount([FromQuery] ReservationParameters reservationParameters)
        {
            // this method retrieves the count of all reservations in order to support pagination
            var reservationCount = _reservationRepository.GetAllReservationsCount(reservationParameters);
            return reservationCount;
        }

        [HttpGet]
        [Route("/api/user-reservations")]
        [Authorize(Roles = "Registered user")]
        public IActionResult GetAllUserReservations([FromQuery] ReservationParameters reservationParameters)
        {
            // this method retrieves all reservations for a specified registered user and supports 
            // pagination, filtering and sorting
            var reservationCount = _reservationRepository.GetAllUserReservations(reservationParameters);
            return Ok(reservationCount.Result);
        }

        [HttpGet]
        [Route("/api/user-reservations/count")]
        [Authorize(Roles = "Registered user")]
        public IActionResult GetAllUserReservationsCount ([FromQuery] ReservationParameters reservationParameters)
        {
            // this method retrieves the count of all user reservations in order to support pagination for
            // user specific reservations
            var reservationCount = _reservationRepository.GetAllUserReservationsCount(reservationParameters);
            return Ok(reservationCount.Result);
        }
    }
}
