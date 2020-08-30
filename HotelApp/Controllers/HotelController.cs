using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using HotelApp.API.DbContexts.Entities;
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
        private readonly IHotelStatusRepository _hotelStatusRepository;

        public HotelController(IHotelRepository hotelRepository,
                               IHotelStatusRepository hotelStatusRepository)
        {
            _hotelRepository = hotelRepository;
            _hotelStatusRepository = hotelStatusRepository;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        public async Task<IActionResult> RegisterHotel([FromBody] RegisterHotelDTO model)
        {
            ClaimsPrincipal currentUser = User;
            await _hotelRepository.CreateHotelAsync(model,
            _hotelStatusRepository.GetHotelStatusById((int)HotelStatusTypes.Pending),
            currentUser);

            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel registered successfully!"
            });
        }

        [HttpPut]
        [Authorize(Roles = "Hotel manager")]
        [Route("{id}")]
        public async Task<IActionResult> EditHotel(int id, [FromBody] RegisterHotelDTO model)
        {
            var hotel = _hotelRepository.GetHotelById(id);
            await _hotelRepository.UpdateHotelAsync(model, hotel);
            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel updated successfully!"
            });
        }
    }
}
