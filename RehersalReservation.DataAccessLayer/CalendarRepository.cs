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
    public class CalendarRepository : BaseRepository, ICalendarRepository
    {
        public async Task CreateReservation(Reservation reservation)
        {
            SqlParameter[] parameters =
   {
                    new SqlParameter("@Start", SqlDbType.DateTime) { Value =  reservation.Start},
                    new SqlParameter("@End", SqlDbType.DateTime) { Value =  reservation.End},
                    new SqlParameter("@Title", SqlDbType.NVarChar) { Value =  reservation.Title},
                    new SqlParameter("@RoomId", SqlDbType.Int) { Value =  reservation.RoomId},
                    new SqlParameter("@Total", SqlDbType.Decimal) { Value =  reservation.Total},
                    new SqlParameter("@PrimaryColor", SqlDbType.NVarChar) { Value =  reservation.PrimaryColor},
                    new SqlParameter("@SecondaryColor", SqlDbType.NVarChar) { Value =  reservation.SecondaryColor},
                    new SqlParameter("@CustomerId", SqlDbType.Int) { Value =  reservation.CustomerId},
                };
            await ExecuteProcedure("CreateReservation", parameters);
        }

        public async Task<List<ReservationVM>> GetCustomerReservations(int customerId)
        {
            List < ReservationVM> reservations = new List<ReservationVM>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetCustomerReservations", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@CustomerId", SqlDbType.Int) { Value = customerId}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        ReservationVM reservation = new ReservationVM();
                        reservation.Id = int.Parse(rdr["Id"].ToString());
                        reservation.Title = rdr["Title"].ToString();
                        reservation.Start = DateTime.Parse(rdr["Start"].ToString());
                        reservation.End = DateTime.Parse(rdr["End"].ToString());
                        reservation.Total = decimal.Parse(rdr["Total"].ToString());
                        reservation.Room = rdr["Title"].ToString();
                        reservation.Path = rdr["PathURL"].ToString();
                        reservation.ImageURL = rdr["ImageURL"].ToString();
                        reservation.Description = rdr["Description"].ToString();
                        reservation.Address = rdr["Address"].ToString();
                        reservation.StartProgram = rdr["StartProgram"].ToString();
                        reservation.EndProgram = rdr["EndProgram"].ToString();
                       reservations.Add(reservation);

                    }
                }
            }
            return reservations;
        }

        public async Task<ReservationVM> GetReservationById(int id)
        {
            ReservationVM reservation = new ReservationVM();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetReservationById", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@ReservationId", SqlDbType.Int) { Value = id}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        reservation.Id = int.Parse(rdr["Id"].ToString());
                        reservation.Title = rdr["Title"].ToString();
                        reservation.Start = DateTime.Parse(rdr["Start"].ToString());
                        reservation.End = DateTime.Parse(rdr["End"].ToString());
                        reservation.Total = decimal.Parse(rdr["Total"].ToString());
                        reservation.Room = rdr["Title"].ToString();
                        reservation.Path = rdr["PathURL"].ToString();
                        reservation.ImageURL = rdr["ImageURL"].ToString();
                        reservation.Description = rdr["Description"].ToString();
                        reservation.Address = rdr["Address"].ToString();
                        reservation.StartProgram = rdr["StartProgram"].ToString();
                        reservation.EndProgram = rdr["EndProgram"].ToString();

                    }
                }
            }
            return reservation;
        }

        public async Task<List<Reservation>> GetReservationsByRoomId(int roomId)
        {
            List <Reservation> reservations = new List<Reservation>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("GetReservationsByRoomId", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@RoomId", SqlDbType.Int) { Value = roomId}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Id = int.Parse(rdr["Id"].ToString());
                        reservation.Title = rdr["Title"].ToString();
                        reservation.Start = DateTime.Parse(rdr["Start"].ToString());
                        reservation.End = DateTime.Parse(rdr["End"].ToString());
                        reservation.Total = decimal.Parse(rdr["Total"].ToString());
                        reservation.PrimaryColor = rdr["PrimaryColor"].ToString();
                        reservation.SecondaryColor = rdr["SecondaryColor"].ToString();
                        reservation.CustomerId = int.Parse(rdr["CustomerId"].ToString());
                        reservations.Add(reservation);
                    }
                }
            }
            return reservations;
        }

        public async Task<List<Reservation>> SearchReservations(string title)
        {
            List<Reservation> reservations = new List<Reservation>();
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SearchReservations", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter[] parameters =
                {
                new SqlParameter("@Title", SqlDbType.NVarChar) { Value = title}
                };
                cmd.Parameters.AddRange(parameters.ToArray());
                using (SqlDataReader rdr = await cmd.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        Reservation reservation = new Reservation();
                        reservation.Id = int.Parse(rdr["Id"].ToString());
                        reservation.Title = rdr["Title"].ToString();
                        reservation.Start = DateTime.Parse(rdr["Start"].ToString());
                        reservation.End = DateTime.Parse(rdr["End"].ToString());
                        reservation.Total = decimal.Parse(rdr["Total"].ToString());
                        reservation.PrimaryColor = rdr["PrimaryColor"].ToString();
                        reservation.SecondaryColor = rdr["SecondaryColor"].ToString();
                        reservation.CustomerId = int.Parse(rdr["CustomerId"].ToString());
                        reservations.Add(reservation);
                    }
                }
            }
            return reservations;
        }
    }
}
