namespace ProductCatalogApp.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Brand { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; } = String.Empty;
    }
}