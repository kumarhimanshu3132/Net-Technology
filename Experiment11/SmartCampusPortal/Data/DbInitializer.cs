using System;
using System.Data.SqlClient;

namespace SmartCampusPortal.Data
{
    public static class DbInitializer
    {
        public static void Initialize(string masterConnectionString, string defaultConnectionString)
        {
            // 1. Ensure Database B2026 exists
            try
            {
                using var masterConn = new SqlConnection(masterConnectionString);
                masterConn.Open();
                var checkDbQuery = @"
                    IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'B2026')
                    BEGIN
                        CREATE DATABASE [B2026];
                    END";
                using var cmd = new SqlCommand(checkDbQuery, masterConn);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DbInitializer] Master connection notice: {ex.Message}");
            }

            // 2. Ensure Table CampusServiceTbl exists and seed 5 sample campus requests
            using (var conn = new SqlConnection(defaultConnectionString))
            {
                conn.Open();
                var checkTableQuery = @"
                    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CampusServiceTbl')
                    BEGIN
                        CREATE TABLE CampusServiceTbl (
                            TicketId INT IDENTITY(1,1) PRIMARY KEY,
                            StudentName VARCHAR(100) NOT NULL,
                            RoomOrBlock VARCHAR(50) NOT NULL,
                            ServiceCategory VARCHAR(50) NOT NULL,
                            Description VARCHAR(250) NOT NULL,
                            Priority VARCHAR(20) NOT NULL,
                            Status VARCHAR(30) NOT NULL,
                            CreatedDate DATETIME NOT NULL
                        );

                        INSERT INTO CampusServiceTbl (StudentName, RoomOrBlock, ServiceCategory, Description, Priority, Status, CreatedDate)
                        VALUES
                        ('Aarav Sharma', 'Hostel Block A - 204', 'Wi-Fi', 'Wi-Fi connectivity drops frequently in the evening hours during study time.', 'High', 'Open', DATEADD(DAY, -2, GETDATE())),
                        ('Priya Patel', 'Hostel Block B - 112', 'Hostel Maintenance', 'Water leakage from bathroom faucet on 1st floor corridor.', 'Medium', 'In Progress', DATEADD(DAY, -1, GETDATE())),
                        ('Rohan Verma', 'Lab 3 - Computer Block', 'Lab Issue', 'Desktop PC #18 display flickering continuously and network cable loose.', 'High', 'Open', DATEADD(HOUR, -12, GETDATE())),
                        ('Ananya Iyer', 'Admin Block - Desk 4', 'ID Card', 'Smart RFID campus ID card replacement request due to chip damage.', 'Low', 'Resolved', DATEADD(DAY, -3, GETDATE())),
                        ('Kabir Mehta', 'Central Library - 2nd Floor', 'Library', 'Automated book return kiosk scanner is not detecting student barcodes.', 'Medium', 'Open', DATEADD(HOUR, -4, GETDATE()));
                    END";

                using var cmd = new SqlCommand(checkTableQuery, conn);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
