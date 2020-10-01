using HotelApp.API.DbContexts.Entities;
using System.Collections.Generic;

namespace HotelApp.API.Models
{
    public class AddRoomDTO
    {
        public string Name { get; set; }
        public int NumberOfBeds { get; set; }
        public int Price { get; set; }
        public int HotelId { get; set; }
    }
}
