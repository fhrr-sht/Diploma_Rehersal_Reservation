using RehersalReservation.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer.Contracts
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomers();
        Task DeleteCustomer(int customerID);
        Task UpdateCustomer(Customer customer);
        Task<Customer> GetCustomerByID(int customerID);
        Task InsertCustomer(Customer customer);
    }
}
