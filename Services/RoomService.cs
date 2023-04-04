using Entity;
using RehersalReservation.DataAccessLayer.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RoomService : IRoomService
    {
        private IRoomRepository roomRepository;
        public RoomService(IRoomRepository roomRepository)
        {
            this.roomRepository = roomRepository;
        }
        public async Task<List<Room>> GetListOfRooms()
        {

            var listOfRooms = await roomRepository.GetAllRooms();
            List<Room> rooms = listOfRooms.Select(o =>
           new Room
           {
               RoomId = o.RehersalRoomID,
               RehersalSpaceID = o.RehersalSpaseID,
               RehersalRoomName = o.RehersalRoomName,
               RehersalRoomSize = o.RehersalRoomSize,
               ImageURL = o.ImageURL,
               PathURL = o.PathURL,
               PriceHour = o.PriceHour,
               StartProgram = o.StartProgram,
               Address = o.Address,
               Description = o.Description,
               EndProgram = o.EndProgram,

           }).ToList();

            return rooms;
        }
        public async Task DeleteRoom(int roomID)
        {
            await roomRepository.DeleteRoom(roomID);
        }

        public async Task<Room> GetRoomByID(int roomID)
        {
            RehersalReservation.DataAccessLayer.Models.Room data = await this.roomRepository.GetRoomByID(roomID);
            Room rehersalRoom = new Room
            {
                RoomId = data.RehersalRoomID,
                RehersalSpaceID = data.RehersalSpaseID,
                RehersalRoomName = data.RehersalRoomName,
                RehersalRoomSize = data.RehersalRoomSize,
                ImageURL = data.ImageURL,
                PathURL = data.PathURL,
                PriceHour = data.PriceHour,
                StartProgram = data.StartProgram,
                Address= data.Address,
                Description= data.Description,
                EndProgram = data.EndProgram,
            };
            return rehersalRoom;
        }

        public async Task<List<Room>> GetRoomByRehersalID(int rehersalSpaceID)
        {
            IEnumerable<RehersalReservation.DataAccessLayer.Models.Room> data = await this.roomRepository.GetRoomByRehersalID(rehersalSpaceID);
            List<Room> rooms = data.Select(o =>
            new Room
            {
                RoomId = o.RehersalRoomID,
                RehersalSpaceID = o.RehersalSpaseID,
                RehersalRoomName = o.RehersalRoomName,
                RehersalRoomSize = o.RehersalRoomSize,
                ImageURL = o.ImageURL,
                PathURL = o.PathURL,
                PriceHour = o.PriceHour,
                StartProgram = o.StartProgram,
                Address = o.Address,
                Description = o.Description,
                EndProgram = o.EndProgram,
            }).ToList();
            return rooms;
        }

        public async Task<List<Room>> GetRooms()
        {
            IEnumerable<RehersalReservation.DataAccessLayer.Models.Room> data = await this.roomRepository.GetRooms();
            List<Room> rooms = data.Select(o =>
            new Room
            {
                RoomId = o.RehersalRoomID,
                RehersalSpaceID = o.RehersalSpaseID,
                RehersalRoomName = o.RehersalRoomName,
                RehersalRoomSize = o.RehersalRoomSize,
                ImageURL = o.ImageURL,
                PathURL = o.PathURL,
                PriceHour = o.PriceHour,
                StartProgram = o.StartProgram,
                Address = o.Address,
                Description = o.Description,
                EndProgram = o.EndProgram,
            }).ToList();
            return rooms;
        }

        public async Task InsertRoom(Room room)
        {
            await roomRepository.InsertRoom(new RehersalReservation.DataAccessLayer.Models.Room
            {
                RehersalSpaseID = room.RehersalSpaceID,
                RehersalRoomName = room.RehersalRoomName,
                RehersalRoomSize = room.RehersalRoomSize,
                ImageURL = room.ImageURL,
                PathURL = room.PathURL,
                PriceHour = room.PriceHour,
                Address = room.Address,
                Description = room.Description,
                EndProgram = room.EndProgram,
                StartProgram= room.StartProgram,
                RehersalRoomID = room.RoomId,
                
                
            });
        }

        public async Task UpdateRoom(Room room)
        {
            await roomRepository.UpdateRoom(new RehersalReservation.DataAccessLayer.Models.Room
            {
                RehersalSpaseID = room.RehersalSpaceID,
                RehersalRoomName = room.RehersalRoomName,
                RehersalRoomSize = room.RehersalRoomSize,
                ImageURL = room.ImageURL,
                PathURL = room.PathURL,
                PriceHour = room.PriceHour,
                Address = room.Address,
                Description = room.Description,
                EndProgram = room.EndProgram,
                StartProgram = room.StartProgram,
                RehersalRoomID = room.RoomId,

            });
        }
    }
}
