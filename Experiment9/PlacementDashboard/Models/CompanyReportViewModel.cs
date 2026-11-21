namespace PlacementDashboard.Models
{
    public class CompanyReportItem
    {
        public string CompanyName { get; set; } = string.Empty;
        public int TotalCandidates { get; set; }
        public int PlacedCount { get; set; }
        public int PendingCount { get; set; }
        public int RejectedCount { get; set; }
        public decimal HighestPackage { get; set; }
        public decimal AveragePackage { get; set; }
        public string RolesOffered { get; set; } = string.Empty;
    }

    public class CompanyReportViewModel
    {
        public List<CompanyReportItem> CompanySummaries { get; set; } = new();
        public PlacementSummary OverallSummary { get; set; } = new();
        public DateTime GeneratedAt { get; set; } = DateTime.Now;
    }
}
