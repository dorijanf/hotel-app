using AutoMapper;
using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;

namespace HotelApp.API.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<RegisterHotelDTO, Hotel>();
            CreateMap<AddRoomDTO, Room>();
            CreateMap<ReservationDTO, Reservation>();
        }
    }
}
