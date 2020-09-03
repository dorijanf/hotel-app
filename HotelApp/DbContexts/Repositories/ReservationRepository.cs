using AutoMapper;
using HotelApp.API.Configuration;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly IMapper _mapper;
        private readonly UserResolverService _userResolverService;
        private readonly UserManager<User> _userManager;
        private readonly ISort<Reservation> _sort;

        public ReservationRepository(HotelAppContext hotelAppContext,
                                     IMapper mapper,
                                     UserResolverService userResolverService,
                                     UserManager<User> userManager,
                                     ISort<Reservation> sort)
        {
            _hotelAppContext = hotelAppContext;
            _mapper = mapper;
            _userResolverService = userResolverService;
            _userManager = userManager;
            _sort = sort;
        }

        public async Task<int> CreateReservationAsync(ReservationDTO model, int roomId)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            var reservation = _mapper.Map<Reservation>(model);
            reservation.CreationDate = DateTime.Now;
            reservation.RegisteredUser = user;
            reservation.ReservationStatusId = (int)ReservationStatusTypes.Processing;
            reservation.RoomId = roomId;
            _hotelAppContext.Reservations.Add(reservation);
            _hotelAppContext.SaveChanges();
            return reservation.Id;
        }

        public IEnumerable<Reservation> GetAllReservations(ReservationParameters reservationParameters)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reservations = _hotelAppContext.HotelUsers.Where(x => x.UserId == currentUserName)
                                                          .Select(x => _hotelAppContext.Rooms
                                                          .Where(y => y.HotelId == x.HotelId))
                                                          .SelectMany(z => z
                                                          .SelectMany(y => y.Reservations));
            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return PagedList<Reservation>.ToPagedList(reservations,
                reservationParameters.PageNumber,
                reservationParameters.PageSize);

        }

        public IEnumerable<Reservation> GetAllReservations(int roomId, ReservationParameters reservationParameters)
        {
            var reservations = _hotelAppContext.Reservations.Where(x => x.RoomId == roomId);
            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return PagedList<Reservation>.ToPagedList(reservations,
                reservationParameters.PageNumber,
                reservationParameters.PageSize);
        }

        public Reservation GetReservationById(int id)
        {
            return _hotelAppContext.Reservations.Find(id);
        }

        public void UpdateReservationStatus(int reservationId, int statusId)
        {
            var reservation = _hotelAppContext.Reservations.Find(reservationId);
            reservation.ReservationStatusId = statusId;

            _hotelAppContext.Reservations.Update(reservation);
            _hotelAppContext.SaveChanges();
        }

        private IQueryable<Reservation> FilterReservations(ref IQueryable<Reservation> reservations, ReservationParameters reservationParameters)
        {
            if(reservationParameters.ReservationStatus != null)
            {
                reservations = reservations.Where(r => r.ReservationStatus.Name == reservationParameters.ReservationStatus);
            }
            
            return reservations;
        }
    }
}
