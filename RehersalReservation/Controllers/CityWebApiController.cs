using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using RehersalReservation.Models;
using RehersalReservation.Attributes;


namespace RehersalReservation.Controllers
{
    [Route("api/City")]
    [AllowCrossSiteJson]
    public class CityWebApiController : ApiController
    {
        private ICityService cityService;
        private IRehersalService rehersalService;
        public CityWebApiController(ICityService cityService, IRehersalService rehersalService)
        {
            this.cityService = cityService;
            this.rehersalService = rehersalService;
        }
        [HttpGet]
        [Route("api/City/Cities")]
        [ResponseType(typeof(List<City>))]
        public async Task<IHttpActionResult> GetCities()
        {
            IEnumerable<Entity.City> data = await this.cityService.GetCities();
            IEnumerable<City> cities = data.Select(o =>
            new City
            {
                CityID = o.CityID,
                CityName = o.CityName
            }).ToList();
            return Ok(cities);
        }
        [HttpDelete]
        [Route("api/City/Delete/{id}")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Delete(int id)
        {
            IEnumerable<Entity.RehersalSpase> data = await this.rehersalService.GetRehersalByCityID(id);
            if (data == null)
            {
                return NotFound();
            }
            if (data.Count() == 0)
            {
                await cityService.DeleteCity(id);
            }
            else
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpPut]
        [Route("api/City/Edit")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Edit([FromBody]City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await cityService.UpdateCity(new Entity.City
            {
                CityID = city.CityID,
                CityName = city.CityName
            });
            return Ok(); 
        }
        [HttpPost]
        [Route("api/City/Create")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Create(City city)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await cityService.InsertCity(new Entity.City
            {
                CityName = city.CityName
            });
            return Ok();
        }
    }
}
