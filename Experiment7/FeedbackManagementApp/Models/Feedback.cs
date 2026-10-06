using System.ComponentModel.DataAnnotations;
namespace FeedbackManagementApp.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be at least 3 characters long.")]
        [RegularExpression(@"^[a-zA-Z]{2,}( [a-zA-Z]{2,})*$",
            ErrorMessage = "Only alphabets (A-Z, a-z) allowed with single space between words. Single-letter words are not allowed.")]
        [Display(Name = "Student / User Name")]
        public string UserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
            ErrorMessage = "Please enter a valid email address (e.g., student@domain.com).")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a category.")]
        [Display(Name = "Feedback Category")]
        public string Category { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please select a rating on the scale.")]
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        [Display(Name = "Rating Scale (1 to 5)")]
        public int Rating { get; set; }
        [Required(ErrorMessage = "Feedback cannot be empty.")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Feedback must be at least 10 characters long.")]
        [RegularExpression(@"^[a-zA-Z\s.,!?'""-]+$",
            ErrorMessage = "Only characters/words are allowed. Numbers or numeric values cannot be entered.")]
        [Display(Name = "Your Feedback")]
        public string Comments { get; set; } = string.Empty;
        public DateTime SubmittedAt { get; set; } = DateTime.Now;
    }
}