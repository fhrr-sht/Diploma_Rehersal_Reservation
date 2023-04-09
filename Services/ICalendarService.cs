using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICalendarService
    {
        Task<List<Reservation>> GetReservationsByRoomId(int roomId);
        Task CreateReservation(Reservation reservation);
        Task<List<ReservationVM>> GetReservationById(int id);
        Task<List<Reservation>> SearchReservations(string title);
    }
}
