using System.ComponentModel.DataAnnotations;

namespace SmartCampusPortal.Models
{
    public class StatusUpdateDto
    {
        [Required(ErrorMessage = "Status is required")]
        [StringLength(30)]
        public string Status { get; set; } = string.Empty;
    }
}
