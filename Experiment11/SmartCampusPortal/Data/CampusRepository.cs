using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Data
{
    public interface ICampusRepository
    {
        IEnumerable<CampusServiceTicket> GetAllTickets(string? category = null);
        CampusServiceTicket? GetTicketById(int ticketId);
        CampusServiceTicket CreateTicket(CampusServiceTicket ticket);
        bool UpdateTicketStatus(int ticketId, string status);
        bool DeleteTicket(int ticketId);
    }

    public class CampusRepository : ICampusRepository
    {
        private readonly string _connectionString;

        public CampusRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=B2026;Integrated Security=True;TrustServerCertificate=True;";
        }

        public IEnumerable<CampusServiceTicket> GetAllTickets(string? category = null)
        {
            var list = new List<CampusServiceTicket>();
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"SELECT TicketId, StudentName, RoomOrBlock, ServiceCategory, Description, Priority, Status, CreatedDate 
                             FROM CampusServiceTbl";

            if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                query += " WHERE ServiceCategory = @Category";
            }

            query += " ORDER BY TicketId DESC";

            using var cmd = new SqlCommand(query, conn);
            if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                cmd.Parameters.AddWithValue("@Category", category);
            }

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(MapTicket(reader));
            }

            return list;
        }

        public CampusServiceTicket? GetTicketById(int ticketId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"SELECT TicketId, StudentName, RoomOrBlock, ServiceCategory, Description, Priority, Status, CreatedDate 
                             FROM CampusServiceTbl 
                             WHERE TicketId = @TicketId";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TicketId", ticketId);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return MapTicket(reader);
            }

            return null;
        }

        public CampusServiceTicket CreateTicket(CampusServiceTicket ticket)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"INSERT INTO CampusServiceTbl (StudentName, RoomOrBlock, ServiceCategory, Description, Priority, Status, CreatedDate)
                             OUTPUT INSERTED.TicketId
                             VALUES (@StudentName, @RoomOrBlock, @ServiceCategory, @Description, @Priority, @Status, @CreatedDate)";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentName", ticket.StudentName);
            cmd.Parameters.AddWithValue("@RoomOrBlock", ticket.RoomOrBlock);
            cmd.Parameters.AddWithValue("@ServiceCategory", ticket.ServiceCategory);
            cmd.Parameters.AddWithValue("@Description", ticket.Description);
            cmd.Parameters.AddWithValue("@Priority", string.IsNullOrWhiteSpace(ticket.Priority) ? "Medium" : ticket.Priority);
            cmd.Parameters.AddWithValue("@Status", string.IsNullOrWhiteSpace(ticket.Status) ? "Open" : ticket.Status);
            cmd.Parameters.AddWithValue("@CreatedDate", ticket.CreatedDate == default ? DateTime.Now : ticket.CreatedDate);

            ticket.TicketId = (int)cmd.ExecuteScalar();
            return ticket;
        }

        public bool UpdateTicketStatus(int ticketId, string status)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"UPDATE CampusServiceTbl 
                             SET Status = @Status 
                             WHERE TicketId = @TicketId";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@Status", status);
            cmd.Parameters.AddWithValue("@TicketId", ticketId);

            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        public bool DeleteTicket(int ticketId)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            string query = @"DELETE FROM CampusServiceTbl WHERE TicketId = @TicketId";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@TicketId", ticketId);

            int rows = cmd.ExecuteNonQuery();
            return rows > 0;
        }

        private static CampusServiceTicket MapTicket(SqlDataReader reader)
        {
            return new CampusServiceTicket
            {
                TicketId = reader.GetInt32(reader.GetOrdinal("TicketId")),
                StudentName = reader.GetString(reader.GetOrdinal("StudentName")),
                RoomOrBlock = reader.GetString(reader.GetOrdinal("RoomOrBlock")),
                ServiceCategory = reader.GetString(reader.GetOrdinal("ServiceCategory")),
                Description = reader.GetString(reader.GetOrdinal("Description")),
                Priority = reader.GetString(reader.GetOrdinal("Priority")),
                Status = reader.GetString(reader.GetOrdinal("Status")),
                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate"))
            };
        }
    }
}
