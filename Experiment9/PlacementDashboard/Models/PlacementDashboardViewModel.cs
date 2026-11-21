namespace PlacementDashboard.Models
{
    public class PlacementDashboardViewModel
    {
        public List<Placement> Placements { get; set; } = new();
        public PlacementSummary Summary { get; set; } = new();

        public string? SearchTerm { get; set; }
        public string? StatusFilter { get; set; }
        public string? SortBy { get; set; }
        public string? SortOrder { get; set; }

        public List<string> StatusOptions { get; set; } = new() { "All", "Placed", "Pending", "Rejected" };
    }
}
