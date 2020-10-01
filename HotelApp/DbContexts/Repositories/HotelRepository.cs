using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using System;
using HotelApp.API.Configuration;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using HotelApp.API.Extensions.Exceptions;
using System.Security.Cryptography.X509Certificates;

namespace HotelApp.API.DbContexts.Repositories
{
    /*
     * A repository that handles hotel creation/deletion and edit functionalities
     * The repository also retrivies hotels in various different ways (retrieves a list of hotels
     * for a specific manager, all hotels, single hotel by id/name)
     */
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly UserResolverService _userResolverService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ILogger<HotelRepository> _logger;

        public HotelRepository(HotelAppContext hotelAppContext,
                               UserResolverService userResolverService,
                               UserManager<User> userManager,
                               IMapper mapper,
                               ILogger<HotelRepository> logger)
        {
            _hotelAppContext = hotelAppContext;
            _userResolverService = userResolverService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> CreateHotelAsync(RegisterHotelDTO model)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);
            var city = CreateCity(model.CityName);
            if (!_userManager.IsInRoleAsync(user, "Hotel manager").Result)
            {
                await _userManager.AddToRoleAsync(user, "Hotel manager");
            }
            model.StatusId = (int)HotelStatusTypes.Pending;

            var hotelManager = new HotelUser();

            model.CityId = city.Id;
            var hotel = _mapper.Map<Hotel>(model);
            hotel.CityId = city.Id;
            hotelManager.Hotel = hotel;
            hotelManager.User = user;

            _hotelAppContext.HotelUsers.Add(hotelManager);
            hotel.HotelUsers.Add(hotelManager);
            _hotelAppContext.Hotels.Add(hotel);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Hotel successfully added!");
            return hotel.Id;
        }

        public ICollection<Hotel> GetAllHotelsWithSameName(string name)
        {
            return _hotelAppContext.Hotels.Where(h => h.Name == name).ToList();
        }

        public ICollection<Hotel> GetHotels()
        {
            return _hotelAppContext.Hotels.ToList();
        }

        public ICollection<Hotel> GetAllUnconfirmedHotels()
        {
            return _hotelAppContext.Hotels.Where(x => x.StatusId == 3)
                              .ToList();
        }

        public IEnumerable<Hotel> GetAllHotelsForUser()
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var hotels = _hotelAppContext.HotelUsers.Where(x => x.UserId == currentUserId)
                                                          .Select(x => _hotelAppContext.Hotels
                                                          .Where(y => y.Id == x.HotelId))
                                                          .SelectMany(y => y);
            return hotels;

        }

        public Hotel GetHotelById(int id)
        {
            var hotel = _hotelAppContext.Hotels.Find(id);
            if(hotel == null)
            {
                throw new NotFoundException($"The hotel with ID {id} could not be found.");
            }
            return hotel;
        }

        public Hotel GetHotelByName(string name)
        {
            return _hotelAppContext.Hotels.FirstOrDefault(x => x.Name == name);
        }

        public void UpdateHotelAsync(int hotelId, RegisterHotelDTO model)
        {
            var hotel = GetHotelById(hotelId);

            if (!model.StatusId.HasValue)
            {
                model.StatusId = hotel.StatusId;
            }

            hotel = _mapper.Map(model, hotel);
            _hotelAppContext.Hotels.Update(hotel);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Hotel successfully updated!");
        }

        public void UpdateHotelStatus(int hotelId, int statusId)
        {
            var hotel = GetHotelById(hotelId);
            hotel.StatusId = statusId;
            _hotelAppContext.Hotels.Update(hotel);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Hotel status successfully updated!");
        }

        private City CreateCity(string name)
        {
            var city = GetCityByName(name);
            if (city == null)
            {
                city = new City
                {
                    Name = name
                };
                _hotelAppContext.Cities.Add(city);
                _hotelAppContext.SaveChanges();
                _logger.LogInformation("A new city has been successfully created!");
            }
            return city;
        }

        private City GetCityByName(string name)
        {
            var city = _hotelAppContext.Cities.FirstOrDefault(c => c.Name == name);
            return city;
        }

        private City GetCityById(int id)
        {
            var city = _hotelAppContext.Cities.Find(id);
            if(city == null)
            {
                throw new NotFoundException($"The city with ID {id} could not be found.");
            }
            return city;
        }

        public City GetHotelCityName(int id)
        {
            var city = GetCityById(id);
            return city;
        }
    }
}
