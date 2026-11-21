using PlacementDashboard.Models;

namespace PlacementDashboard.Data
{
    public interface IPlacementDataAccess
    {
        List<Placement> GetAll(string? searchTerm = null, string? statusFilter = null, string? sortBy = null, string? sortOrder = null);
        Placement? GetById(int id);
        int Insert(Placement placement);
        int Update(Placement placement);
        int Delete(int id);
        PlacementSummary GetSummary();
        List<CompanyReportItem> GetCompanyReport();
    }
}
