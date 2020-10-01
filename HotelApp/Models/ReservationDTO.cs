using HotelApp.API.Configuration;
using System;
namespace HotelApp.API.Models
{
    public class ReservationDTO
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Note { get; set; }
    }
}
