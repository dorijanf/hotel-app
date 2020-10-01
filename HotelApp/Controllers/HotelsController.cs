using System.Linq;
using System.Threading.Tasks;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace HotelApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelsController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        public IActionResult RegisterHotel([FromBody] RegisterHotelDTO model)
        {
            // creates a hotel and assigns the currently logged in user the hotel manager role
            var hotelId = _hotelRepository.CreateHotelAsync(model).Result;
            return Ok(hotelId);
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
        [Route("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] RegisterHotelDTO model)
        {
            // updates hotel data and requires 5 strings (hotel name, contact number, email, address, city name) 
            _hotelRepository.UpdateHotelAsync(id, model);
            return Ok("Hotel successfully updated!");
        }

        [HttpPut]
        [Authorize(Roles ="SuperAdministrator, Administrator")]
        [Route("{id}/status")]
        public IActionResult UpdateHotelStatus(int id, [FromBody] int statusId)
        {
            // updates hotel status by assigning an integer (status id) to the status id property of the hotel
            _hotelRepository.UpdateHotelStatus(id, statusId);
            return Ok("Hotel status successfully updated!");
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IActionResult GetSingleHotel(int id)
        {
            // gets a single hotel by the id of the hotel
            var hotel = _hotelRepository.GetHotelById(id);
            return Ok(hotel);
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        public IActionResult GetAllHotels()
        {
            // gets a list of all hotels for a specific user that is in the role of a hotel manager
            var hotels = _hotelRepository.GetAllHotelsForUser();
            return Ok(hotels);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{hotelId}/city-name/{cityId}")]
        public IActionResult GetHotelCityName([FromRoute] int cityId)
        {
            // gets the name of the city where the hotel is located
            var city = _hotelRepository.GetHotelCityName(cityId);
            return Ok(city);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [Route("pending")]
        public IActionResult GetAllUnconfirmedHotels()
        {
            // gets a list of all unconfirmed hotels 
            var hotels = _hotelRepository.GetAllUnconfirmedHotels();
            return Ok(hotels);
        }
    }
}
