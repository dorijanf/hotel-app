using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using HotelApp.API.DbContexts;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.DbContexts.Repositories;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly IHotelRepository _hotelRepository;
        private readonly IHotelStatusRepository _hotelStatusRepository;
        private readonly UserManager<User> _userManager;

        public HotelController(IHotelRepository hotelRepository,
                               IHotelStatusRepository hotelStatusRepository,
                               UserManager<User> userManager)
        {
            _hotelRepository = hotelRepository;
            _hotelStatusRepository = hotelStatusRepository;
            _userManager = userManager;
        }

        [HttpPost]
        [Authorize(Roles = "Registered user")]
        [Route("register")]
        public async Task<IActionResult> RegisterHotel([FromBody] RegisterHotelDTO model)
        {
            var hotelExists = _hotelRepository.GetHotelByName(model.Name);
            if (hotelExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ResponseDTO
                    {
                        Status = "Error",
                        Message = "A hotel with that name already exists!"
                    });
            }

            ClaimsPrincipal currentUser = User;
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            if (_userManager.IsInRoleAsync(user, "Registered user").Result)
            {
                await _userManager.AddToRoleAsync(user, "Hotel manager");
            }

            _hotelRepository.CreateHotel(model,
            _hotelStatusRepository.GetHotelStatusById((int)HotelStatusTypes.Pending),
            user);

            return Ok(new ResponseDTO
            {
                Status = "Success",
                Message = "Hotel registered successfully!"
            });
        }
    }
}
