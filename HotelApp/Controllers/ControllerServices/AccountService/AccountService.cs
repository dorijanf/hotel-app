//using HotelApp.API.Configuration;
//using HotelApp.API.DbContexts.Entities;
//using HotelApp.API.Extensions.Exceptions;
//using HotelApp.API.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.IO;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace HotelApp.API.DbContexts.Services
//{
//    /* 
//     * AccountService is a class that contains methods that are concerned with account 
//     * registration/removal, role assignment/deassignment and the retrieval of user data.
//     * It is also used to assign JWT tokens to logged in users and to describe JWT settings.
//     */
//    public class AccountService : IAccountService
//    {
//        private readonly UserManager<User> _userManager;
//        private readonly RoleManager<UserRole> _roleManager;
//        private readonly JwtSettings _jwtSettings;
//        private readonly HotelAppContext _hotelAppContext;
//        private readonly ILogger<AccountService> _logger;

//        public AccountService(
//            UserManager<User> userManager,
//            RoleManager<UserRole> roleManager,
//            JwtSettings jwtSettings,
//            HotelAppContext hotelAppContext,
//            ILogger<AccountService> logger)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//            _jwtSettings = jwtSettings;
//            _hotelAppContext = hotelAppContext;
//            _logger = logger;
//        }

//        public async Task<LoginResponseDTO> Login(LoginUserDTO model)
//        {
//            var user = await _userManager.FindByNameAsync(model.UserName);
//            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
//            {
//                var token = await CreateTokenAsync(user);
//                var loginInfo = new LoginResponseDTO
//                {
//                    UserName = user.UserName,
//                    Token = token.Token,
//                    Expiration = token.Expiration,
//                    isRegistered = await _userManager.IsInRoleAsync(user, "Registered user"),
//                    isManager = await _userManager.IsInRoleAsync(user, "Hotel manager"),
//                    isAdmin = await _userManager.IsInRoleAsync(user, "Administrator"),
//                    isSuperAdmin = await _userManager.IsInRoleAsync(user, "SuperAdministrator")
//                };
//                _logger.LogInformation("User logged in successfully!");
//                if (loginInfo != null)
//                {
//                    return loginInfo;
//                }
//            }
//            throw new NotAuthorizedException("Invalid username or password.");
//        }

//        public async Task Register(RegisterUserDTO model)
//        {
//            var userExists = await _userManager.FindByNameAsync(model.UserName);
//            if (userExists != null)
//            {
//                throw new BadRequestException("A user with that username already exists!");
//            }

//            var user = new User()
//            {
//                Email = model.Email,
//                SecurityStamp = Guid.NewGuid().ToString(),
//                UserName = model.UserName,
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);
//            if (!result.Succeeded)
//            {
//                throw new NotFoundException("Not found");
//            }

//            await _userManager.AddToRoleAsync(user, "Registered user");

//            if (await _roleManager.RoleExistsAsync("Registered user"))
//            {

//            }
            
//            if(string.IsNullOrEmpty(user.Id))
//            {
//                throw new BadRequestException("Failed to create user.");
//            }

//            _logger.LogInformation("User registered successfully!");
//        }

//        public async Task<User[]> GetAllAdmins()
//        {
//            var users = await _userManager.GetUsersInRoleAsync("Administrator");
//            List<User> admins = new List<User>(users);
//            var adminList = admins.ToArray();
//            return adminList;
//        }

//        private async Task<TokenResult> CreateTokenAsync(User user)
//        {
//            var claims = new List<Claim>
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
//                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
//            };
//            var userRoles = await _userManager.GetRolesAsync(user);
//            claims.AddRange(userRoles.Select(x => new Claim(ClaimTypes.Role, x)));
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
//            var expires = DateTime.Now.AddDays(7);
//            var token = new JwtSecurityToken(
//                _jwtSettings.Issuer,
//                _jwtSettings.Audience,
//                claims,
//                expires: expires,
//                signingCredentials: creds
//            );
//            return new TokenResult
//            {
//                Token = new JwtSecurityTokenHandler().WriteToken(token),
//                Expiration = expires
//            };
//        }

//        public async Task DeleteAdministrator(string id)
//        {
//            var user = await _userManager.FindByIdAsync(id);
//            var userRoles = await _userManager.GetRolesAsync(user);

//            if (userRoles.Count() > 0)
//            {
//                foreach (var role in userRoles.ToList())
//                {
//                    await _userManager.RemoveFromRoleAsync(user, role);
//                }
//            }
//            _hotelAppContext.Users.Remove(user);
//            await _userManager.DeleteAsync(user);
//            _logger.LogInformation("Administrator successfully deleted!");
//        }

//        public async Task RegisterAdmin(RegisterUserDTO model)
//        {
//            var userExists = await _userManager.FindByNameAsync(model.UserName);

//            if (userExists != null)
//            {
//                throw new BadRequestException("User with that username already exists!");
//            }
//            var user = new User()
//            {
//                Email = model.Email,
//                SecurityStamp = Guid.NewGuid().ToString(),
//                UserName = model.UserName,
//            };

//            var result = await _userManager.CreateAsync(user, model.Password);
//            if (!result.Succeeded)
//            {
//                throw new BadRequestException("Something went wrong, check registration details and try again.");
//            }

//            if (await _roleManager.RoleExistsAsync("Administrator"))
//            {
//                await _userManager.AddToRoleAsync(user, "Administrator");
//            }

//            _logger.LogInformation("Administrator successfully registered!");
//        }
//    }
//}
