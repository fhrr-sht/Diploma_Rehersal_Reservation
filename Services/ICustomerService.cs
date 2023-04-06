using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICustomerService
    {
        Task<Customer> AuthenticateAsync(string name, string password);
        Task<Customer> Register(Customer customer, string password);
        Task<List<ReservationVM>> GetCustomerReservations(int customerID);
    }
}
