using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using InventoryManagementApp.Models;
namespace InventoryManagementApp.Controllers
{
    public class InventoryController : Controller
    {
        // Connection string as per our MSSQLLocalDB setup
        string conString = @"Data Source=(localdb)\MSSQLLocalDB; Database=B2026; Integrated Security=True";
        // READ: Display all items
        public IActionResult Index()
        {
            List<Inventory> invList = new List<Inventory>();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM InventoryTbl", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    invList.Add(new Inventory
                    {
                        ItemId = Convert.ToInt32(rdr["ItemId"]),
                        ItemName = rdr["ItemName"].ToString(),
                        Category = rdr["Category"].ToString(),
                        Price = Convert.ToDecimal(rdr["Price"]),
                        Stock = Convert.ToInt32(rdr["Stock"])
                    });
                }
            }
            return View(invList);
        }
        // CREATE: GET & POST
        [HttpGet]
        public IActionResult Create() => View();
        [HttpPost]
        public IActionResult Create(Inventory model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO InventoryTbl (ItemName, Category, Price, Stock) VALUES (@Name, @Cat, @Price, @Stock)", con);
                    cmd.Parameters.AddWithValue("@Name", model.ItemName);
                    cmd.Parameters.AddWithValue("@Cat", model.Category);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@Stock", model.Stock);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // UPDATE: GET & POST
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Inventory model = new Inventory();
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM InventoryTbl WHERE ItemId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    model.ItemId = Convert.ToInt32(rdr["ItemId"]);
                    model.ItemName = rdr["ItemName"].ToString();
                    model.Category = rdr["Category"].ToString();
                    model.Price = Convert.ToDecimal(rdr["Price"]);
                    model.Stock = Convert.ToInt32(rdr["Stock"]);
                }
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Inventory model)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(conString))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE InventoryTbl SET ItemName=@Name, Category=@Cat, Price=@Price, Stock=@Stock WHERE ItemId=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", model.ItemId);
                    cmd.Parameters.AddWithValue("@Name", model.ItemName);
                    cmd.Parameters.AddWithValue("@Cat", model.Category);
                    cmd.Parameters.AddWithValue("@Price", model.Price);
                    cmd.Parameters.AddWithValue("@Stock", model.Stock);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
        // DELETE: POST
        [HttpPost]
        public IActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM InventoryTbl WHERE ItemId = @Id", con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
    }
}