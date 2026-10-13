using System.ComponentModel.DataAnnotations;
namespace InventoryManagementApp.Models
{
    public class Inventory
    {
        public int ItemId { get; set; }
        [Required(ErrorMessage = "Item Name is required")]
        public string ItemName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}