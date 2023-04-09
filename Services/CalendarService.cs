using Entity;
using RehersalReservation.DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CalendarService : ICalendarService
    {
        private readonly ICalendarRepository calendarRepository;

        public CalendarService(ICalendarRepository calendarRepository)
        {
            this.calendarRepository = calendarRepository;
        }

        public async Task CreateReservation(Reservation reservation)
        {
            await calendarRepository.CreateReservation(new RehersalReservation.DataAccessLayer.Models.Reservation
            {
                RoomId = reservation.RoomId,
                CustomerId = reservation.CustomerId,
                End = reservation.End,
                PrimaryColor = reservation.PrimaryColor,
                SecondaryColor = reservation.SecondaryColor,
                Start = reservation.Start,
                Title = reservation.Title,
                Total = reservation.Total,
            });
        }

        public async Task<List<ReservationVM>> GetReservationById(int id)
        {
            var reservation = await calendarRepository.GetReservationById(id);
            return new List<ReservationVM>() { new ReservationVM 
                {
                Address= reservation.Address,
                Description= reservation.Description,
                End= reservation.End,
                EndProgram= reservation.EndProgram,
                Room = reservation.Room,
                Id= reservation.Id,
                ImageURL= reservation.ImageURL,
                Name= reservation.Name,
                Path= reservation.Path,
                Start= reservation.Start,
                StartProgram= reservation.StartProgram,
                Title= reservation.Title,
                Total  = reservation.Total
                }
            };
        }

        public async Task<List<Reservation>> GetReservationsByRoomId(int roomId)
        {
            var data = await calendarRepository.GetReservationsByRoomId(roomId);
            List<Reservation> reservations = data.Select(o =>
             new Reservation
            {
                 Total= o.Total,
                 Title= o.Title,
                 Start= o.Start,
                 CustomerId= o.CustomerId,
                 End= o.End,
                 RoomId= o.RoomId,
                 Id= o.RoomId,
                 PrimaryColor= o.PrimaryColor,
                 SecondaryColor = o.SecondaryColor
            }).ToList();

            return reservations;
        }

        public async Task<List<Reservation>> SearchReservations(string title)
        {
            var data = await calendarRepository.SearchReservations(title);
            List<Reservation> reservations = data.Select(o =>
             new Reservation
             {
                 Total = o.Total,
                 Title = o.Title,
                 Start = o.Start,
                 CustomerId = o.CustomerId,
                 End = o.End,
                 RoomId = o.RoomId,
                 Id = o.RoomId,
                 PrimaryColor = o.PrimaryColor,
                 SecondaryColor = o.SecondaryColor
             }).ToList();

            return reservations;
        }
    }
}
