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
        public async Task<IActionResult> RegisterHotel([FromBody] RegisterHotelDTO model)
        {
            var hotelId = await _hotelRepository.CreateHotelAsync(model);

            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel registered successfully!",
                EntityId = hotelId
            });
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
        [Route("{id}")]
        public IActionResult UpdateHotel(int id, [FromBody] RegisterHotelDTO model)
        {
            _hotelRepository.UpdateHotelAsync(id, model);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel updated successfully!"
            });
        }

        [HttpPut]
        [Authorize(Roles ="SuperAdministrator, Administrator")]
        [Route("{id}/status")]
        public IActionResult UpdateHotelStatus(int id, [FromBody] int statusId)
        {
            _hotelRepository.UpdateHotelStatus(id, statusId);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel status updated successfully!"
            });
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IActionResult GetSingleHotel(int id)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);
            }

            return StatusCode(StatusCodes.Status404NotFound,
                new ResponseDTO
                {
                    Status = "Error",
                    Message = "Hotel does not exist."
                });
        }

        [HttpGet]
        [Authorize(Roles = "Hotel manager")]
        public IActionResult GetAllHotels([FromBody] int statusId)
        {
            var hotels = _hotelRepository.GetAllHotelsForUser();
            return Ok(hotels);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdministrator, Administrator")]
        [Route("pending")]
        public IActionResult GetAllUnconfirmedHotels()
        {
            var hotels = _hotelRepository.GetAllUnconfirmedHotels();
            return Ok(hotels);
        }
    }
}
