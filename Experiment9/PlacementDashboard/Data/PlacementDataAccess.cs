using System.Data;
using System.Data.SqlClient;
using PlacementDashboard.Models;

namespace PlacementDashboard.Data
{
    public class PlacementDataAccess : IPlacementDataAccess
    {
        private readonly string _connectionString;

        public PlacementDataAccess(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                ?? "Server=(localdb)\\MSSQLLocalDB;Database=B2026;Integrated Security=True;TrustServerCertificate=True;";
        }

        public List<Placement> GetAll(string? searchTerm = null, string? statusFilter = null, string? sortBy = null, string? sortOrder = null)
        {
            var list = new List<Placement>();

            string query = @"SELECT PlacementId, StudentName, EnrollmentNo, CompanyName, Role, PackageLPA, Status 
                             FROM PlacementTbl WHERE 1=1 ";

            using var conn = new SqlConnection(_connectionString);
            using var cmd = new SqlCommand();
            cmd.Connection = conn;

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query += " AND (StudentName LIKE @search OR CompanyName LIKE @search OR EnrollmentNo LIKE @search OR Role LIKE @search) ";
                cmd.Parameters.AddWithValue("@search", "%" + searchTerm.Trim() + "%");
            }

            if (!string.IsNullOrWhiteSpace(statusFilter) && !statusFilter.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                query += " AND Status = @status ";
                cmd.Parameters.AddWithValue("@status", statusFilter.Trim());
            }

            bool isDescending = string.Equals(sortOrder, "desc", StringComparison.OrdinalIgnoreCase);
            string orderDirection = isDescending ? "DESC" : "ASC";

            string orderColumn = (sortBy?.ToLower()) switch
            {
                "name" or "studentname" => "StudentName",
                "enrollment" or "enrollmentno" => "EnrollmentNo",
                "company" or "companyname" => "CompanyName",
                "role" => "Role",
                "package" or "packagelpa" => "PackageLPA",
                "status" => "Status",
                _ => "PlacementId"
            };

            if (orderColumn == "PlacementId" && string.IsNullOrEmpty(sortOrder))
            {
                query += " ORDER BY PlacementId DESC";
            }
            else
            {
                query += $" ORDER BY {orderColumn} {orderDirection}";
            }

            cmd.CommandText = query;
            conn.Open();

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Placement
                {
                    PlacementId = Convert.ToInt32(reader["PlacementId"]),
                    StudentName = reader["StudentName"].ToString() ?? string.Empty,
                    EnrollmentNo = reader["EnrollmentNo"].ToString() ?? string.Empty,
                    CompanyName = reader["CompanyName"].ToString() ?? string.Empty,
                    Role = reader["Role"].ToString() ?? string.Empty,
                    PackageLPA = Convert.ToDecimal(reader["PackageLPA"]),
                    Status = reader["Status"].ToString() ?? string.Empty
                });
            }

            return list;
        }

        public Placement? GetById(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            string query = @"SELECT PlacementId, StudentName, EnrollmentNo, CompanyName, Role, PackageLPA, Status 
                             FROM PlacementTbl WHERE PlacementId = @id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Placement
                {
                    PlacementId = Convert.ToInt32(reader["PlacementId"]),
                    StudentName = reader["StudentName"].ToString() ?? string.Empty,
                    EnrollmentNo = reader["EnrollmentNo"].ToString() ?? string.Empty,
                    CompanyName = reader["CompanyName"].ToString() ?? string.Empty,
                    Role = reader["Role"].ToString() ?? string.Empty,
                    PackageLPA = Convert.ToDecimal(reader["PackageLPA"]),
                    Status = reader["Status"].ToString() ?? string.Empty
                };
            }

            return null;
        }

        public int Insert(Placement placement)
        {
            using var conn = new SqlConnection(_connectionString);
            string query = @"INSERT INTO PlacementTbl (StudentName, EnrollmentNo, CompanyName, Role, PackageLPA, Status)
                             VALUES (@StudentName, @EnrollmentNo, @CompanyName, @Role, @PackageLPA, @Status);
                             SELECT SCOPE_IDENTITY();";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@StudentName", placement.StudentName);
            cmd.Parameters.AddWithValue("@EnrollmentNo", placement.EnrollmentNo);
            cmd.Parameters.AddWithValue("@CompanyName", placement.CompanyName);
            cmd.Parameters.AddWithValue("@Role", placement.Role);
            cmd.Parameters.AddWithValue("@PackageLPA", (object?)placement.PackageLPA ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", placement.Status);

            conn.Open();
            object result = cmd.ExecuteScalar();
            return Convert.ToInt32(result);
        }

        public int Update(Placement placement)
        {
            using var conn = new SqlConnection(_connectionString);
            string query = @"UPDATE PlacementTbl 
                             SET StudentName = @StudentName,
                                 EnrollmentNo = @EnrollmentNo,
                                 CompanyName = @CompanyName,
                                 Role = @Role,
                                 PackageLPA = @PackageLPA,
                                 Status = @Status
                             WHERE PlacementId = @PlacementId";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@PlacementId", placement.PlacementId);
            cmd.Parameters.AddWithValue("@StudentName", placement.StudentName);
            cmd.Parameters.AddWithValue("@EnrollmentNo", placement.EnrollmentNo);
            cmd.Parameters.AddWithValue("@CompanyName", placement.CompanyName);
            cmd.Parameters.AddWithValue("@Role", placement.Role);
            cmd.Parameters.AddWithValue("@PackageLPA", (object?)placement.PackageLPA ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@Status", placement.Status);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public int Delete(int id)
        {
            using var conn = new SqlConnection(_connectionString);
            string query = "DELETE FROM PlacementTbl WHERE PlacementId = @id";

            using var cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@id", id);

            conn.Open();
            return cmd.ExecuteNonQuery();
        }

        public PlacementSummary GetSummary()
        {
            var summary = new PlacementSummary();

            using var conn = new SqlConnection(_connectionString);
            string query = @"
                SELECT 
                    COUNT(*) AS TotalStudents,
                    ISNULL(SUM(CASE WHEN Status = 'Placed' THEN 1 ELSE 0 END), 0) AS PlacedCount,
                    ISNULL(SUM(CASE WHEN Status = 'Pending' THEN 1 ELSE 0 END), 0) AS PendingCount,
                    ISNULL(SUM(CASE WHEN Status = 'Rejected' THEN 1 ELSE 0 END), 0) AS RejectedCount,
                    ISNULL(MAX(PackageLPA), 0) AS HighestPackage,
                    ISNULL(AVG(PackageLPA), 0) AS AveragePackage
                FROM PlacementTbl";

            using var cmd = new SqlCommand(query, conn);
            conn.Open();

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                summary.TotalStudents = Convert.ToInt32(reader["TotalStudents"]);
                summary.PlacedCount = Convert.ToInt32(reader["PlacedCount"]);
                summary.PendingCount = Convert.ToInt32(reader["PendingCount"]);
                summary.RejectedCount = Convert.ToInt32(reader["RejectedCount"]);
                summary.HighestPackage = Convert.ToDecimal(reader["HighestPackage"]);
                summary.AveragePackage = Math.Round(Convert.ToDecimal(reader["AveragePackage"]), 2);
            }

            return summary;
        }

        public List<CompanyReportItem> GetCompanyReport()
        {
            var allPlacements = GetAll();

            var report = allPlacements
                .GroupBy(p => p.CompanyName, StringComparer.OrdinalIgnoreCase)
                .Select(g => new CompanyReportItem
                {
                    CompanyName = g.Key,
                    TotalCandidates = g.Count(),
                    PlacedCount = g.Count(p => p.Status.Equals("Placed", StringComparison.OrdinalIgnoreCase)),
                    PendingCount = g.Count(p => p.Status.Equals("Pending", StringComparison.OrdinalIgnoreCase)),
                    RejectedCount = g.Count(p => p.Status.Equals("Rejected", StringComparison.OrdinalIgnoreCase)),
                    HighestPackage = g.Any() ? g.Max(p => p.PackageLPA ?? 0m) : 0,
                    AveragePackage = g.Any() ? Math.Round(g.Average(p => p.PackageLPA ?? 0m), 2) : 0,
                    RolesOffered = string.Join(", ", g.Select(p => p.Role).Distinct())
                })
                .OrderByDescending(r => r.PlacedCount)
                .ThenByDescending(r => r.HighestPackage)
                .ToList();

            return report;
        }
    }
}
