namespace PlacementDashboard.Models
{
    public class PlacementSummary
    {
        public int TotalStudents { get; set; }
        public int PlacedCount { get; set; }
        public int PendingCount { get; set; }
        public int RejectedCount { get; set; }
        public decimal HighestPackage { get; set; }
        public decimal AveragePackage { get; set; }
        public decimal PlacementRate => TotalStudents > 0 ? Math.Round(((decimal)PlacedCount / TotalStudents) * 100, 1) : 0;
    }
}
