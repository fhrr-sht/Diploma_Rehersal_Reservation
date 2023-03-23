using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer.Models
{
    public class RoomFVM
    {
        public int RoomFVMId { get; set; }
        public string Title { get; set; }
        public string ImageURL { get; set; }
        public string PathURL { get; set; }
        public decimal PriceHour { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string StartProgram { get; set; }
        public string EndProgram { get; set; }
    }
}
