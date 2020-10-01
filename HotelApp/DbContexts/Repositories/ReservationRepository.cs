using AutoMapper;
using HotelApp.API.Configuration;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Extensions.Exceptions;
using HotelApp.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    /*
     * This is a repository that contains method create, delete and update the status of room reservations.
     * It also retrieves reservations specifically for registered users and hotel managers. This is needed because
     * we need to retrieve only those reservations which the user created himself and the hotel manager needs to see
     * all reservations made on the rooms that he is responsible for.
     */
    public class ReservationRepository : IReservationRepository
    {
        private readonly HotelAppContext _hotelAppContext;
        private readonly IMapper _mapper;
        private readonly UserResolverService _userResolverService;
        private readonly UserManager<User> _userManager;
        private readonly ISort<Reservation> _sort;
        private readonly ILogger<ReservationRepository> _logger;
        public ReservationRepository(HotelAppContext hotelAppContext,
                                     IMapper mapper,
                                     UserResolverService userResolverService,
                                     UserManager<User> userManager,
                                     ISort<Reservation> sort,
                                     ILogger<ReservationRepository> logger)
        {
            _hotelAppContext = hotelAppContext;
            _mapper = mapper;
            _userResolverService = userResolverService;
            _userManager = userManager;
            _sort = sort;
            _logger = logger;
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
            _logger.LogInformation("Reservation successfully created!");
            return reservation.Id;
        }

        public IEnumerable<Reservation> GetAllReservations(ReservationParameters reservationParameters)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reservations = _hotelAppContext.HotelUsers.Where(x => x.UserId == currentUserId)
                                                          .Select(x => _hotelAppContext.Rooms
                                                          .Where(y => y.HotelId == x.HotelId))
                                                          .SelectMany(z => z.SelectMany(y => y.Reservations));

            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return PagedList<Reservation>.ToPagedList(reservations,
                reservationParameters.PageNumber,
                reservationParameters.PageSize);
        }

        public int GetAllReservationsCount(ReservationParameters reservationParameters)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var reservations = _hotelAppContext.HotelUsers.Where(x => x.UserId == currentUserId)
                                                          .Select(x => _hotelAppContext.Rooms
                                                          .Where(y => y.HotelId == x.HotelId))
                                                          .SelectMany(z => z.SelectMany(y => y.Reservations));

            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return reservations.Count();
        }

        public async Task<int> GetAllUserReservationsCount(ReservationParameters reservationParameters)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            var reservations = _hotelAppContext.Reservations.Where(x => x.RegisteredUserId == user.Id);
            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return reservations.Count();
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

        public async Task<IEnumerable<Reservation>> GetAllUserReservations(ReservationParameters reservationParameters)
        {
            var currentUser = _userResolverService.GetUser();
            var currentUserName = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(currentUserName);

            var reservations = _hotelAppContext.Reservations.Where(x => x.RegisteredUserId == user.Id);
            reservations = FilterReservations(ref reservations, reservationParameters);
            reservations = _sort.ApplySort(reservations, reservationParameters.OrderBy);
            return PagedList<Reservation>.ToPagedList(reservations,
                reservationParameters.PageNumber,
                reservationParameters.PageSize);
        }

        public Reservation GetReservationById(int id)
        {
            var reservation =  _hotelAppContext.Reservations.Find(id);
            if(reservation == null)
            {
                throw new NotFoundException($"The reservation with ID {id} could not be found.");
            }
            return reservation;
        }

        public void UpdateReservationStatus(int reservationId, int statusId)
        {
            var reservation = _hotelAppContext.Reservations.Find(reservationId);
            if (statusId == 4)
            {
                if ((reservation.DateFrom - DateTime.Now).TotalDays > 3)
                {
                    reservation.ReservationStatusId = statusId;
                }
                else
                {
                    throw new BadRequestException("You cannot cancel this reservation because the cancellation period has ended.");
                }
            }
            else
            {
                reservation.ReservationStatusId = statusId;
            }

            _hotelAppContext.Reservations.Update(reservation);
            _hotelAppContext.SaveChanges();
            _logger.LogInformation("Reservation status successfully updated!");
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
