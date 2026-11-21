using System.Data.SqlClient;

namespace PlacementDashboard.Data
{
    public static class DbInitializer
    {
        public static void Initialize(string connectionString)
        {
            try
            {
                var builder = new SqlConnectionStringBuilder(connectionString);
                string targetDatabase = builder.InitialCatalog;
                builder.InitialCatalog = "master";
                string masterConnectionString = builder.ConnectionString;

                using (var masterConn = new SqlConnection(masterConnectionString))
                {
                    masterConn.Open();
                    string checkDbSql = $"IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = '{targetDatabase}') CREATE DATABASE [{targetDatabase}];";
                    using var createDbCmd = new SqlCommand(checkDbSql, masterConn);
                    createDbCmd.ExecuteNonQuery();
                }

                using (var dbConn = new SqlConnection(connectionString))
                {
                    dbConn.Open();
                    string createTableSql = @"
                        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name = 'PlacementTbl' AND xtype = 'U')
                        BEGIN
                            CREATE TABLE PlacementTbl (
                                PlacementId INT IDENTITY(1,1) PRIMARY KEY,
                                StudentName VARCHAR(100) NOT NULL,
                                EnrollmentNo VARCHAR(30) NOT NULL,
                                CompanyName VARCHAR(100) NOT NULL,
                                Role VARCHAR(100) NOT NULL,
                                PackageLPA DECIMAL(10,2) NOT NULL,
                                Status VARCHAR(30) NOT NULL
                            );
                        END";

                    using (var createTableCmd = new SqlCommand(createTableSql, dbConn))
                    {
                        createTableCmd.ExecuteNonQuery();
                    }

                    string checkRowsSql = "SELECT COUNT(*) FROM PlacementTbl";
                    using (var checkRowsCmd = new SqlCommand(checkRowsSql, dbConn))
                    {
                        int rowCount = (int)checkRowsCmd.ExecuteScalar();
                        if (rowCount == 0)
                        {
                            string seedSql = @"
                                INSERT INTO PlacementTbl (StudentName, EnrollmentNo, CompanyName, Role, PackageLPA, Status) VALUES
                                ('Aarav Sharma', '21BECE0101', 'Tata Consultancy Services', 'Systems Engineer', 4.50, 'Placed'),
                                ('Priya Patel', '21BECE0102', 'Infosys', 'Specialist Programmer', 9.50, 'Placed'),
                                ('Rohan Mehta', '21BECE0103', 'Microsoft', 'Software Engineer', 42.00, 'Placed'),
                                ('Ananya Iyer', '21BECE0104', 'Amazon', 'SDE-1', 28.50, 'Placed'),
                                ('Vikram Singh', '21BECE0105', 'Wipro', 'Project Engineer', 4.00, 'Rejected'),
                                ('Sneha Kulkarni', '21BECE0106', 'Reliance Jio', 'Software Developer', 7.50, 'Placed'),
                                ('Rahul Verma', '21BECE0107', 'L&T Technology Services', 'Embedded Engineer', 5.25, 'Pending'),
                                ('Tanvi Desai', '21BECE0108', 'Zomato', 'Associate Product Manager', 14.00, 'Placed');";

                            using var seedCmd = new SqlCommand(seedSql, dbConn);
                            seedCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DbInitializer Warning] Database initialization skipped or encountered error: {ex.Message}");
            }
        }
    }
}
