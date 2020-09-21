using HotelApp.API.DbContexts.Entities;
using System;

namespace HotelApp.API.Models
{
    public class ReservationParameters : BasePagingModel
    {
        public ReservationParameters()
        {
            OrderBy = "CreationDate";
        }

        public DateTime CreationDate { get; set; }
        public string ReservationStatus { get; set; }
    }
}
