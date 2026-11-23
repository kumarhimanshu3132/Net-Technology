using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models
{
    public class CampusServiceTicket
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Student Name is required")]
        [StringLength(100, ErrorMessage = "Student Name cannot exceed 100 characters")]
        [Display(Name = "Student Name")]
        public string StudentName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Room / Block details are required")]
        [StringLength(50, ErrorMessage = "Room/Block cannot exceed 50 characters")]
        [Display(Name = "Room / Block")]
        public string RoomOrBlock { get; set; } = string.Empty;

        [Required(ErrorMessage = "Service Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        [Display(Name = "Service Category")]
        public string ServiceCategory { get; set; } = string.Empty;

        [Required(ErrorMessage = "Issue description is required")]
        [StringLength(250, ErrorMessage = "Description cannot exceed 250 characters")]
        [Display(Name = "Description")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Priority is required")]
        [StringLength(20)]
        public string Priority { get; set; } = "Medium";

        [StringLength(30)]
        public string Status { get; set; } = "Open";

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
