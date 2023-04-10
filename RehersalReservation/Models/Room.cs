using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehersalReservation.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        public string RehersalRoomName { get; set; }
        public int RehersalRoomSize { get; set; }
        public int RehersalSpaseID { get; set; }
        public string ImageURL { get; set; }
        public string PathURL { get; set; }
        public decimal PriceHour { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string StartProgram { get; set; }
        public string EndProgram { get; set; }
    }
}