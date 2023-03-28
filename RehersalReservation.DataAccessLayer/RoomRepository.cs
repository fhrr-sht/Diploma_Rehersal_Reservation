using RehersalReservation.DataAccessLayer.Contracts;
using RehersalReservation.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RehersalReservation.DataAccessLayer
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public async Task<List<Room>> GetAllRooms()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetAllRooms", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        Room room = new Room();
                        room.RehersalRoomID = int.Parse(rdr["RehersalRoomID"].ToString());
                        room.RehersalRoomName = rdr["Title"].ToString();
                        room.ImageURL = rdr["ImageURL"].ToString();
                        room.PathURL = rdr["PathURL"].ToString();
                        room.Description = rdr["Description"].ToString();
                        room.StartProgram = rdr["StartProgram"].ToString();
                        room.EndProgram = rdr["EndProgram"].ToString();
                        room.Address = rdr["Address"].ToString();
                        room.PriceHour = decimal.Parse(rdr["PriceHour"].ToString());
                        room.RehersalSpaseID = int.Parse(rdr["RehersalSpaseID"].ToString());
                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }
        public async Task DeleteRoom(int roomID)
        {
            SqlParameter[] parameters =
              {
                new SqlParameter("@RehersalRoomID", SqlDbType.Int) { Value = roomID}
                };
            await ExecuteProcedure("DeleteRoom", parameters);
        }

        public async Task<List<Room>> GetRooms()
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetRooms", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        Room room = new Room();
                        room.RehersalRoomID = int.Parse(rdr["RehersalRoomID"].ToString());
                        room.RehersalRoomName = rdr["RehersalRoomName"].ToString();
                        //room.RehersalRoomSize = int.Parse(rdr["RehersalRoomSize"].ToString());
                        room.ImageURL = rdr["ImageURL"].ToString();
                        room.PathURL = rdr["PathURL"].ToString();
                        room.Description = rdr["Description"].ToString();
                        room.StartProgram = rdr["StartProgram"].ToString();
                        room.EndProgram = rdr["EndProgram"].ToString();
                        room.Address = rdr["Address"].ToString();
                        room.PriceHour = decimal.Parse(rdr["PriceHour"].ToString());
                        room.RehersalSpaseID = int.Parse(rdr["RehersalSpaseID"].ToString());

                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }

        public async Task<Room> GetRoomByID(int roomID)
        {
            Room room = new Room();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetRoomByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@RehersalRoomID", SqlDbType.Int) { Value = roomID}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        room.RehersalRoomName = rdr["RehersalRoomName"].ToString();
                        room.RehersalRoomID = int.Parse(rdr["RehersalRoomID"].ToString());
                        //room.RehersalRoomSize = int.Parse(rdr["RehersalRoomSize"].ToString());
                        room.ImageURL = rdr["ImageURL"].ToString();
                        room.PathURL = rdr["PathURL"].ToString();
                        room.Description = rdr["Description"].ToString();
                        room.StartProgram = rdr["StartProgram"].ToString();
                        room.EndProgram = rdr["EndProgram"].ToString();
                        room.Address = rdr["Address"].ToString();
                        room.PriceHour = decimal.Parse(rdr["PriceHour"].ToString());
                        room.RehersalSpaseID = int.Parse(rdr["RehersalSpaseID"].ToString());

                    }
                }
            }
            return room;
        }

        public async Task InsertRoom(Room room)
        {
            SqlParameter[] parameters =
               {
                     new SqlParameter("@RehersalRoomName", SqlDbType.NVarChar, 50) { Value =  room.RehersalRoomName},
                     new SqlParameter("@RehersalSpaseID", SqlDbType.Int) { Value =  room.RehersalSpaseID},
                     new SqlParameter("@RehersalRoomSize", SqlDbType.Int) { Value =  room.RehersalRoomSize},
                     new SqlParameter("@ImageURL", SqlDbType.NVarChar, 500) { Value =  room.ImageURL},
                     new SqlParameter("@PriceHour", SqlDbType.Decimal) { Value =  room.PriceHour},
                     new SqlParameter("@PathURL", SqlDbType.NVarChar, 500) { Value =  room.PathURL},
                     new SqlParameter("@Description", SqlDbType.NVarChar) { Value =  room.Description},
                     new SqlParameter("@Address", SqlDbType.NVarChar, 500) { Value =  room.Address},
                     new SqlParameter("@StartProgram", SqlDbType.NVarChar, 500) { Value =  room.StartProgram},
                     new SqlParameter("@EndProgram", SqlDbType.NVarChar, 500) { Value =  room.EndProgram},
                };
            await ExecuteProcedure("InsertRoom", parameters);
        }

        public async Task UpdateRoom(Room room)
        {
            SqlParameter[] parameters =
                {
                    new SqlParameter("@RehersalRoomName", SqlDbType.NVarChar, 50) { Value =  room.RehersalRoomName},
                     new SqlParameter("@RehersalSpaseID", SqlDbType.Int) { Value =  room.RehersalSpaseID},
                     new SqlParameter("@RehersalRoomSize", SqlDbType.Int) { Value =  room.RehersalRoomSize},
                     new SqlParameter("@RehersalRoomID", SqlDbType.Int) { Value =  room.RehersalRoomID},
                     new SqlParameter("@ImageURL", SqlDbType.NVarChar, 500) { Value =  room.ImageURL},
                     new SqlParameter("@PriceHour", SqlDbType.Decimal) { Value =  room.PriceHour},
                     new SqlParameter("@PathURL", SqlDbType.NVarChar, 500) { Value =  room.PathURL},
                     new SqlParameter("@Description", SqlDbType.NVarChar) { Value =  room.Description},
                     new SqlParameter("@Address", SqlDbType.NVarChar, 500) { Value =  room.Address},
                     new SqlParameter("@StartProgram", SqlDbType.NVarChar, 500) { Value =  room.StartProgram},
                     new SqlParameter("@EndProgram", SqlDbType.NVarChar, 500) { Value =  room.EndProgram},
                };
            await ExecuteProcedure("UpdateRoom", parameters);
        }

        public async Task<List<Room>> GetRoomByRehersalID(int rehersalSpaceID)
        {
            List<Room> rooms = new List<Room>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetRoomByRehersalID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@RehersalSpaceID", SqlDbType.Int) { Value = rehersalSpaceID}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        Room room = new Room();
                        room.RehersalRoomID = int.Parse(rdr["RehersalRoomID"].ToString());
                        room.RehersalRoomName = rdr["RehersalRoomName"].ToString();
                        // room.RehersalRoomSize = int.Parse(rdr["RehersalRoomSize"].ToString());
                        room.ImageURL = rdr["ImageURL"].ToString();
                        room.PathURL = rdr["PathURL"].ToString();
                        room.Description = rdr["Description"].ToString();
                        room.StartProgram = rdr["StartProgram"].ToString();
                        room.EndProgram = rdr["EndProgram"].ToString();
                        room.Address = rdr["Address"].ToString();
                        room.PriceHour = decimal.Parse(rdr["PriceHour"].ToString());

                        rooms.Add(room);
                    }
                }
            }
            return rooms;
        }
    }
}
