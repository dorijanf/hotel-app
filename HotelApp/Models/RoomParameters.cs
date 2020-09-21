using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Models
{
    public class RoomParameters : BasePagingModel
    {
        public RoomParameters()
        {
            OrderBy = "Price";
        }
        public string City { get; set; }
        public uint NumberOfBeds { get; set; }
        public DateTime? reservationDate { get; set; }
        public int Price { get; set; }
    }
}
