using Entity;
using RehersalReservation.Attributes;
using RehersalReservation.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;


namespace RehersalReservation.Controllers
{
    [Route("api/Rehersal")]
    [AllowCrossSiteJson]
    public class RehersalWebApiController : ApiController
    {
        private IRehersalService rehersalService;
        private IRoomService roomService;
        public RehersalWebApiController(IRehersalService rehersalService, IRoomService roomService)
        {
            this.rehersalService = rehersalService;
            this.roomService = roomService;
        }
        [HttpGet]
        [Route("api/Rehersal/Rehersals")]
        [ResponseType(typeof(List<RehersalSpace>))]
        public async Task<IHttpActionResult> Rehersals()
        {
            IEnumerable<Entity.RehersalSpase> data = await this.rehersalService.GetRehersals();
            if (data == null)
            {
                return NotFound();
            }
            IEnumerable<RehersalSpace> rehersalSpaces = data.Select(o =>
            new RehersalSpace
            {
                Adress = o.Adress,
                CityID = o.CityID,
                RehersalSpaseID = o.RehersalSpaseID,
                RehersalSpaseName = o.RehersalSpaseName
            }).ToList();
            return Ok(rehersalSpaces);
        }
        [HttpGet]
        [Route("api/Rehersal/GetRehersalByCityID/{cityId}")]
        [ResponseType(typeof(List<RehersalSpace>))]
        public async Task<IHttpActionResult> GetRehersalByCityID(int cityId)
        {
            IEnumerable<Entity.RehersalSpase> data = await this.rehersalService.GetRehersalByCityID(cityId);
            if (data == null)
            {
                return NotFound();
            }
            IEnumerable<RehersalSpace> rehersalSpaces = data.Select(o =>
            new RehersalSpace
            {
                Adress = o.Adress,
                CityID = o.CityID,
                RehersalSpaseID = o.RehersalSpaseID,
                RehersalSpaseName = o.RehersalSpaseName
            }).ToList();
            return Ok(rehersalSpaces);
        }
        [HttpDelete]
        [Route("api/Rehersal/Delete/{id}")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Delete(int id)
        {
            IEnumerable<Entity.Room> data = await this.roomService.GetRoomByRehersalID(id);
            if (data == null)
            {
                return NotFound();
            }
            if (data.Count() == 0)
            {
                await rehersalService.DeleteRehersal(id);
            }
            else
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpPut]
        [Route("api/Rehersal/Edit")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Edit([FromBody] RehersalSpace rehersalSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await rehersalService.UpdateRehersal(new RehersalSpase
            {
                Adress = rehersalSpace.Adress,
                CityID = rehersalSpace.CityID,
                RehersalSpaseID = rehersalSpace.RehersalSpaseID,
                RehersalSpaseName = rehersalSpace.RehersalSpaseName
            });
            return Ok();
        }
        [HttpPost]
        [Route("api/Rehersal/Create")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Add([FromBody] RehersalSpace rehersalSpace)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await rehersalService.InsertRehersal(new RehersalSpase
            {
                Adress = rehersalSpace.Adress,
                CityID = rehersalSpace.CityID,
                RehersalSpaseID = rehersalSpace.RehersalSpaseID,
                RehersalSpaseName = rehersalSpace.RehersalSpaseName
            });
            return Ok();
        }
    }
}