using RehersalReservation.DataAccessLayer.Contracts;
using RehersalReservation.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository() { }

        public async Task DeleteCustomer(int customerID)
        {
            SqlParameter[] parameters =
  {
                new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerID}
                };
            await ExecuteProcedure("DeleteCustomer", parameters);
        }

        public async Task<Customer> GetCustomerByID(int customerID)
        {
            Customer customer = new Customer();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetCustomerByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@CustomerID", SqlDbType.Int) { Value = customerID}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        customer.EmailAddress = rdr["EmailAddress"].ToString();
                        customer.FirstName = rdr["FirstName"].ToString();
                        customer.LastName = rdr["LastName"].ToString();
                        customer.PhoneNo = rdr["PhoneNo"].ToString();
                        customer.PasswordHash = (byte[])(rdr["PasswordHash"]); 
                        customer.PasswordSalt = (byte[])(rdr["PasswordSalt"]); 

                        customer.CustomerId = int.Parse(rdr["CustomerId"].ToString());
                    }
                }
            }
            return customer;
        }

        public async Task<List<Customer>> GetCustomers()
        {
            List<Customer> customers = new List<Customer>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetCustomers", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    Customer customer = new Customer();
                    while (rdr.Read())
                    {
                        customer.EmailAddress = rdr["EmailAddress"].ToString();
                        customer.FirstName = rdr["FirstName"].ToString();
                        customer.LastName = rdr["LastName"].ToString();
                        customer.PhoneNo = rdr["PhoneNo"].ToString();
                        customer.PasswordHash = (byte[])(rdr["PasswordHash"]);
                        customer.PasswordSalt = (byte[])(rdr["PasswordSalt"]);

                        customer.CustomerId = int.Parse(rdr["CustomerId"].ToString());
                        customers.Add(customer);
                    }
                }
            }
            return customers;
        }

        public async Task InsertCustomer(Customer customer)
        {
            SqlParameter[] parameters =
            {
                     new SqlParameter("@EmailAddress", SqlDbType.NVarChar, 500) { Value =  customer.EmailAddress},
                     new SqlParameter("@FirstName", SqlDbType.NVarChar, 500) { Value =  customer.FirstName},
                     new SqlParameter("@LastName", SqlDbType.NVarChar, 500) { Value =  customer.LastName},              
                     new SqlParameter("@PhoneNo", SqlDbType.NVarChar, 500) { Value =  customer.PhoneNo},              
                     new SqlParameter("@PasswordHash", SqlDbType.VarBinary) { Value =  customer.PasswordHash},              
                     new SqlParameter("@PasswordSalt", SqlDbType.VarBinary) { Value =  customer.PasswordSalt},              
            
            };
            await ExecuteProcedure("InsertCustomer", parameters);
        }

        public Task UpdateCustomer(Customer customer)
        {
            throw new NotImplementedException();
        }
    }
}
