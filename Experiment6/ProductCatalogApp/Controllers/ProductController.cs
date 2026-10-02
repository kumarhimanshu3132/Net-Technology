using Microsoft.AspNetCore.Mvc;
using ProductCatalogApp.Models;
using System.Collections.Generic;
using System.Linq;
namespace ProductCatalogApp.Controllers
{
    public class ProductController : Controller
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Gaming Laptop", Brand = "ASUS ROG", Category = "Electronics", Price = 85000, Stock = 12, Description = "High performance laptop with RTX 4060 graphics." },
            new Product { Id = 2, Name = "Wireless Mouse", Brand = "Logitech", Category = "Accessories", Price = 1500, Stock = 45, Description = "Ergonomic 2.4GHz wireless optical mouse." },
            new Product { Id = 3, Name = "Mechanical Keyboard", Brand = "Keychron", Category = "Accessories", Price = 4500, Stock = 20, Description = "RGB mechanical keyboard with hot-swappable switches." },
            new Product { Id = 4, Name = "Noise Cancelling Headphones", Brand = "Sony", Category = "Audio", Price = 19990, Stock = 8, Description = "Industry-leading wireless noise cancellation headphones." }
        };
        public IActionResult Index()
        {
            return View(_products);
        }
        public IActionResult Details(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product newProduct)
        {
            if (ModelState.IsValid)
            {
                newProduct.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;

                _products.Add(newProduct);
                return RedirectToAction(nameof(Index));
            }
            return View(newProduct);
        }
    }
}