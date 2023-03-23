using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer.Models
{
    public class Reservation
    {
        public int? Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Title { get; set; }
        public int RoomId { get; set; }
        public decimal Total { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public int CustomerId { get; set; }
    }
}
