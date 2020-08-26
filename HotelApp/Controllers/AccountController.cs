using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using HotelApp.API.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace HotelApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly JwtSettings _jwtSettings;

        public AccountController(
                    UserManager<User> userManager,
                    JwtSettings jwtSettings)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var token = await CreateTokenAsync(user);
                var loginInfo = new LoginResponseDTO();
                loginInfo.UserName = user.UserName;
                loginInfo.Token = token.Token;
                loginInfo.Expiration = token.Expiration;
                return Ok(loginInfo);
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ResponseDTO
                                    {
                                        Status = "Error",
                                        Message = "User already exists!"
                                    });
            }

            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError,
                                    new ResponseDTO
                                    {
                                        Status = "Error",
                                        Message = "Cannot create user! Please check registration details and try again."
                                    });
            return Ok(new ResponseDTO { Status = "Success", Message = "User created successfully!" });
        }

        private async Task<TokenResult> CreateTokenAsync(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            var userRoles = await _userManager.GetRolesAsync(user);
            claims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(7);
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: expires,
                signingCredentials: creds
            );
            return new TokenResult
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expires
            };
        }

        [Authorize]
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> Test()
        {
            return Ok(new ResponseDTO { Status = "Success", Message = "Neka test poruka" });
        }
    }
}
