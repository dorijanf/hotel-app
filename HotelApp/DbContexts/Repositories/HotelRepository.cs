using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Http;
using HotelApp.API.Configuration;
using System.Collections.Generic;

namespace HotelApp.API.DbContexts.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly UserResolverService _userResolverService;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public HotelRepository(HotelAppContext hotelAppContext,
                               UserResolverService userResolverService,
                               UserManager<User> userManager,
                               IMapper mapper)
        {
            _hotelAppContext = hotelAppContext;
            _userResolverService = userResolverService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<int> CreateHotelAsync(RegisterHotelDTO model)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            if (!_userManager.IsInRoleAsync(user, "Hotel manager").Result)
            {
                await _userManager.AddToRoleAsync(user, "Hotel manager");
            }
            model.StatusId = (int)HotelStatusTypes.Pending;

            var hotelManager = new HotelUser();

            var hotel = _mapper.Map<Hotel>(model);
            hotelManager.Hotel = hotel;
            hotelManager.User = user;

            _hotelAppContext.HotelUsers.Add(hotelManager);
            hotel.HotelUsers.Add(hotelManager);
            _hotelAppContext.Hotels.Add(hotel);
            _hotelAppContext.SaveChanges();
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
            return _hotelAppContext.Hotels.Find(id);
        }

        public Hotel GetHotelByName(string name)
        {
            return _hotelAppContext.Hotels.FirstOrDefault(x => x.Name == name);
        }

        public void UpdateHotelAsync(int hotelId, RegisterHotelDTO model)
        {
            var hotel = GetHotelById(hotelId);
            if(hotel == null)
            {
                throw new NullReferenceException();
            }
            if (!model.StatusId.HasValue)
            {
                model.StatusId = hotel.StatusId;
            }
            hotel = _mapper.Map(model, hotel);
            _hotelAppContext.Hotels.Update(hotel);
            _hotelAppContext.SaveChanges();
        }

        public void UpdateHotelStatus(int hotelId, int statusId)
        {
            var hotel = GetHotelById(hotelId);
            if (hotel == null)
            {
                throw new NullReferenceException();
            }

            hotel.StatusId = statusId;
            _hotelAppContext.Hotels.Update(hotel);
            _hotelAppContext.SaveChanges();
        }
    }
}
