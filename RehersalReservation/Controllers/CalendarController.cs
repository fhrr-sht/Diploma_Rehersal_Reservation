using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;


namespace RehersalReservation.Controllers
{
    
    [Route("api/Calendar")]
    public class CalendarController : ApiController
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }
        [HttpGet]
        [Route("api/Calendar/GetReservations/{roomId}")]
        public async Task<IHttpActionResult> GetReservations(int roomId)
        {
            try
            {
                var reservations = await _calendarService.GetReservationsByRoomId(roomId);
                return Ok(reservations);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Calendar/CreateReservation")]
        public async Task<IHttpActionResult> CreateReservation([FromBody] Entity.Reservation reservation)
        {
            try
            {
                await _calendarService.CreateReservation(reservation);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("api/Calendar/GetReservationById/{id}")]
        public async Task<IHttpActionResult> GetReservationById(int id)
        {
            try
            {
                var reservations = await _calendarService.GetReservationById(id);
                return Ok(reservations);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("api/Calendar/SearchReservations/{title}")]
        public async Task<IHttpActionResult> SearchReservations(string title)
        {
            try
            {
                var reservations = await _calendarService.SearchReservations(title);
                return Ok(reservations);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
