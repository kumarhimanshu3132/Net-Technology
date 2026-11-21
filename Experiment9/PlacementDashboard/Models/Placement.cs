using System.ComponentModel.DataAnnotations;

namespace PlacementDashboard.Models
{
    public class Placement
    {
        [Display(Name = "Placement ID")]
        public int PlacementId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(100, ErrorMessage = "Student Name cannot exceed 100 characters")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Enrollment Number is required")]
        [StringLength(30, ErrorMessage = "Enrollment Number cannot exceed 30 characters")]
        [Display(Name = "Enrollment No")]
        public string EnrollmentNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(100, ErrorMessage = "Company Name cannot exceed 100 characters")]
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Job Role is required")]
        [StringLength(100, ErrorMessage = "Role cannot exceed 100 characters")]
        [Display(Name = "Role / Designation")]
        public string Role { get; set; } = string.Empty;

        [Required(ErrorMessage = "Package (LPA) is required")]
        [Range(0.50, 150.00, ErrorMessage = "Package must be between 0.50 and 150.00 LPA")]
        [Display(Name = "Package (LPA)")]
        public decimal? PackageLPA { get; set; }

        [Required(ErrorMessage = "Placement Status is required")]
        [StringLength(30)]
        [Display(Name = "Status")]
        public string Status { get; set; } = "Placed";
    }
}
