using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RehersalReservation.Models
{
    public class CustomerVM
    {
        public int CustomerId { get; set; }
        public string EmailAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
    }
}