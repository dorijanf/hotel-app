using HotelApp.API.DbContexts.Entities;
using HotelApp.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HotelApp.API.DbContexts.Repositories
{
    public interface IReservationRepository
    {
        Task<int> CreateReservationAsync(ReservationDTO model, int roomId);
        void UpdateReservationStatus(int reservationId, int statusId);
        Reservation GetReservationById(int id);
        IEnumerable<Reservation> GetAllReservations();
        IEnumerable<Reservation> GetAllReservations(int roomId);
    }
}
