using Entity;
using RehersalReservation.DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository customerRepository;
        private readonly ICalendarRepository calendarRepository;
        public CustomerService(ICustomerRepository customerRepository, ICalendarRepository calendarRepository) 
        { 
            this.customerRepository= customerRepository;
            this.calendarRepository= calendarRepository;
        }

        public async Task<Customer> AuthenticateAsync(string email, string password)
        {
            List<RehersalReservation.DataAccessLayer.Models.Customer>  customers = await customerRepository.GetCustomers();

            // check if email and password fields are empty or null
            if (string.IsNullOrEmpty(email) || (string.IsNullOrEmpty(password)))
            {
                return null;
            }

            var customer = customers.SingleOrDefault(x => x.EmailAddress == email);

            // check if customer with email exist
            if (customer == null)
            {
                return null;
            }

            // check if password is correct
            if (!VerifyPasswordHash(password, customer.PasswordHash, customer.PasswordSalt))
            {
                return null;
            }

            return new Entity.Customer { CustomerId = customer .CustomerId,
            EmailAddress = customer.EmailAddress,
            FirstName= customer.FirstName,  
            LastName= customer.LastName,    
            PasswordHash = customer.PasswordHash,
            PasswordSalt = customer.PasswordSalt,   
            PhoneNo = customer.PhoneNo
            
            } ;

        }

        public async Task<Customer> Register(Customer customer, string password)
        {
            List<RehersalReservation.DataAccessLayer.Models.Customer> customers = await customerRepository.GetCustomers();

            // check if password is null or white space
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ApplicationException("Password is required!");
            }

            if (!IsValidEmail(customer.EmailAddress))
            {
                throw new ApplicationException("Please type a valid Email Address!");
            }

            // check if customer with email exist
            if (customers.Any(x => x.EmailAddress == customer.EmailAddress))
            {
                throw new ApplicationException("Customer with " + customer.EmailAddress + " is already taken");
            }

            byte[] passwordHash, passwordSalt;

            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            customer.PasswordHash = passwordHash;
            customer.PasswordSalt = passwordSalt;

            await customerRepository.InsertCustomer(new RehersalReservation.DataAccessLayer.Models.Customer
            {
                CustomerId = customer.CustomerId,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PasswordHash = customer.PasswordHash,
                PasswordSalt = customer.PasswordSalt,
                PhoneNo = customer.PhoneNo

            });

            return new Entity.Customer
            {
                CustomerId = customer.CustomerId,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PasswordHash = customer.PasswordHash,
                PasswordSalt = customer.PasswordSalt,
                PhoneNo = customer.PhoneNo

            }; 
        }

        public async Task<List<ReservationVM>> GetCustomerReservations(int customerId)
        {
            var reservations = await calendarRepository.GetCustomerReservations(customerId);

            return reservations.Select(x=> new ReservationVM { 
            Address= x.Address,
            Description= x.Description,
            End = x.End,
            EndProgram= x.EndProgram,
            Room= x.Room,
            Id= x.Id,
            ImageURL= x.ImageURL,
            Name = x.Name,
            Path= x.Path,
            Start= x.Start,
            StartProgram= x.StartProgram,
            Title= x.Title,
            Total = x.Total
            }).ToList();
        }


        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        bool IsValidEmail(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
