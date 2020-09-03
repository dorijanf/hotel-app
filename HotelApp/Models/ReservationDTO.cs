using HotelApp.API.Configuration;
using System;
using System.Text.Json.Serialization;

namespace HotelApp.API.Models
{
    public class ReservationDTO
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Note { get; set; }
    }
}
