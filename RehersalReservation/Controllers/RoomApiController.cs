using RehersalReservation.Attributes;
using RehersalReservation.Models;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;


namespace RehersalReservation.Controllers
{
    [Route("api/Room")]
    [AllowCrossSiteJson]
    public class RoomApiController : ApiController
    {
        private IRoomService roomService;
        public RoomApiController(IRoomService roomService)
        {
            this.roomService = roomService;
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("api/Room/GetRooms")]
        [ResponseType(typeof(List<Room>))]
        public async Task<IHttpActionResult> GetRooms()
        {
            try
            {
                var data = await roomService.GetListOfRooms();
                IEnumerable<Room> fildVMs = data.Select(o =>
                new Room
                {
                    RehersalRoomName = o.RehersalRoomName,
                    StartProgram = o.StartProgram,
                    ImageURL = o.ImageURL,
                    Address = o.Address,
                    Description = o.Description,
                    EndProgram = o.EndProgram,
                    RoomId = o.RoomId,
                    PathURL = o.PathURL,
                    PriceHour = o.PriceHour,
                    RehersalSpaseID = o.RehersalSpaceID

                }).ToList();
                return Ok(fildVMs);
            }
            catch (ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("api/Room/Rooms")]
        [ResponseType(typeof(List<Room>))]
        public async Task<IHttpActionResult> Rooms()
        {
            {
                IEnumerable<Entity.Room> data = await this.roomService.GetRooms();
                IEnumerable<Room> rooms = data.Select(o =>
                new Room
                {
                    RoomId = o.RoomId,
                    RehersalSpaseID = o.RehersalSpaceID,
                    RehersalRoomName = o.RehersalRoomName,
                    RehersalRoomSize = o.RehersalRoomSize,
                    ImageURL = o.ImageURL,
                    PathURL = o.PathURL,
                    PriceHour = o.PriceHour,
                    Address = o.Address,
                    Description = o.Description,
                    EndProgram = o.EndProgram ,
                    StartProgram = o.StartProgram
                }).ToList();
                return Ok(rooms);
            }
        }
        [HttpDelete]
        [Route("api/Room/Delete/{id}")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await roomService.DeleteRoom(id);
            return Ok();
        }
        [HttpPost]
        [Route("api/Room/Create")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            await roomService.InsertRoom(new Entity.Room
            {
                RehersalSpaceID = room.RehersalSpaseID,
                RehersalRoomName = room.RehersalRoomName,
                RehersalRoomSize = room.RehersalRoomSize,
                Address = room.Address,
                Description = room.Description,
                EndProgram = room.EndProgram,
                ImageURL = room.ImageURL,
                PathURL = room.PathURL,
                PriceHour = room.PriceHour ,
                RoomId = room.RoomId,
                StartProgram = room.StartProgram
            });
            return Ok();
        }
        [HttpPut]
        [Route("api/Room/Edit")]
        [AllowCrossSiteJson]
        public async Task<IHttpActionResult> Edit(Room room)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            await roomService.UpdateRoom(new Entity.Room
            {
                RehersalSpaceID = room.RehersalSpaseID,
                RehersalRoomName = room.RehersalRoomName,
                RehersalRoomSize = room.RehersalRoomSize,
                Address = room.Address,
                Description = room.Description,
                EndProgram = room.EndProgram,
                ImageURL = room.ImageURL,
                PathURL = room.PathURL,
                PriceHour = room.PriceHour,
                RoomId = room.RoomId,
                StartProgram = room.StartProgram
            });
            return Ok();
        }
        [HttpGet]
        [Route("api/Room/GetRoomByRehersalID/{rehersalSpaceID}")]
        [AllowCrossSiteJson]
        [ResponseType(typeof(List<Room>))]
        public async Task<IHttpActionResult> GetRoomByRehersalID(int rehersalSpaceID)
        {
            {
                IEnumerable<Entity.Room> data = await this.roomService.GetRoomByRehersalID(rehersalSpaceID);
                if (data == null)
                {
                    return NotFound();
                }
                IEnumerable<Room> rooms = data.Select(o =>
                new Room
                {
                    RoomId = o.RoomId,
                    RehersalSpaseID = o.RehersalSpaceID,
                    RehersalRoomName = o.RehersalRoomName,
                    RehersalRoomSize = o.RehersalRoomSize,
                    ImageURL = o.ImageURL,
                    PathURL = o.PathURL,
                    PriceHour = o.PriceHour,
                    Address = o.Address,
                    Description = o.Description,
                    EndProgram = o.EndProgram ,
                    StartProgram = o.StartProgram,
                   
                    
                }).ToList();
                return Ok(rooms);
            }
        }
    }
}
