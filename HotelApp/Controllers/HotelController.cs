using System.Threading.Tasks;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;

        public HotelController(IHotelRepository hotelRepository)
        {
            _hotelRepository = hotelRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        public async Task<IActionResult> RegisterHotel([FromBody] RegisterHotelDTO model)
        {
            await _hotelRepository.CreateHotelAsync(model);

            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel registered successfully!"
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
    }
}
