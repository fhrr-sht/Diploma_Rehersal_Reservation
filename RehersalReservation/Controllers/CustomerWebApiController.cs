using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.IdentityModel.Tokens;
using RehersalReservation.Models;
using Services;

namespace RehersalReservation.Controllers
{
    [Authorize]
    [Route("api/Customer")]
    public class CustomerWebApiController : ApiController
    {
        private readonly ICustomerService customerService;
        const string Secret = "RehersalReservation";
        public CustomerWebApiController(ICustomerService customerService) 
        { 
            this.customerService = customerService;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("api/Customer/Register")]
        public async Task<IHttpActionResult> Register([FromBody] CustomerVM customerVM)
        {

            var customer = new Entity.Customer {
                CustomerId = customerVM.CustomerId,
                EmailAddress = customerVM.EmailAddress,
                FirstName = customerVM.FirstName,
                LastName = customerVM.LastName,
                PhoneNo = customerVM.PhoneNo
            };

            try
            {
                await customerService.Register(customer, customerVM.Password);
                return Ok();
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("api/Customer/Authenticate")]
        public async Task<IHttpActionResult> Authenticate([FromBody] CustomerVM customerVM)
        {
            var customer = await customerService.AuthenticateAsync(customerVM.EmailAddress, customerVM.Password);

            if (customer == null)
            {
                return BadRequest("Email or Password is incorrect! ");
            }

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, customer.CustomerId.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // return customer information without password + token
            return Ok(new
            {
                CustomerId = customer.CustomerId,
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNo = customer.PhoneNo,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Customer/GetCustomerReservations/{customerId}")]
        public async Task<IHttpActionResult> GetCustomerReservations(int customerId)
        {
            try
            {
                var reservations = await customerService.GetCustomerReservations(customerId);
                return Ok(reservations);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
