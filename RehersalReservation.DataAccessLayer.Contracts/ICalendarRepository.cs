using RehersalReservation.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer.Contracts
{
    public interface ICalendarRepository
    {
        Task<List<Reservation>> GetReservationsByRoomId(int roomId);
        Task CreateReservation(Reservation reservation);
        Task<ReservationVM> GetReservationById(int id);
        Task<List<ReservationVM>> GetCustomerReservations(int customerId);
        Task<List<Reservation>> SearchReservations(string title);
    }
}
