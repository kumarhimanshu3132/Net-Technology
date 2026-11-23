using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using SmartCampusPortal.Models;

namespace SmartCampusPortal.Controllers
{
    public class PortalController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public PortalController(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        private string GetApiBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            if (request != null)
            {
                return $"{request.Scheme}://{request.Host}";
            }
            return _configuration["ApiSettings:BaseUrl"] ?? "http://localhost:5063";
        }

        // GET: / or /Portal/Index?category=Wi-Fi
        public async Task<IActionResult> Index(string? category)
        {
            var client = _httpClientFactory.CreateClient();
            string apiUrl = $"{GetApiBaseUrl()}/api/campus";

            List<CampusServiceTicket> tickets = new();
            try
            {
                var response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    tickets = await response.Content.ReadFromJsonAsync<List<CampusServiceTicket>>() ?? new();
                }
                else
                {
                    ViewBag.ErrorMessage = $"Failed to fetch data from API (Status: {response.StatusCode})";
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Error communicating with Campus REST API: {ex.Message}";
            }

            // Category filter
            ViewBag.SelectedCategory = category ?? "All";
            if (!string.IsNullOrWhiteSpace(category) && !category.Equals("All", StringComparison.OrdinalIgnoreCase))
            {
                tickets = tickets.Where(t => t.ServiceCategory.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Summary metrics
            ViewBag.TotalCount = tickets.Count;
            ViewBag.OpenCount = tickets.Count(t => t.Status.Equals("Open", StringComparison.OrdinalIgnoreCase));
            ViewBag.InProgressCount = tickets.Count(t => t.Status.Equals("In Progress", StringComparison.OrdinalIgnoreCase));
            ViewBag.ResolvedCount = tickets.Count(t => t.Status.Equals("Resolved", StringComparison.OrdinalIgnoreCase));
            ViewBag.HighPriorityCount = tickets.Count(t => t.Priority.Equals("High", StringComparison.OrdinalIgnoreCase));

            return View(tickets);
        }

        // GET: /Portal/Create
        [HttpGet]
        public IActionResult Create()
        {
            var model = new CampusServiceTicket
            {
                Priority = "Medium",
                Status = "Open",
                CreatedDate = DateTime.Now
            };
            return View(model);
        }

        // POST: /Portal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampusServiceTicket ticket)
        {
            if (!ModelState.IsValid)
            {
                return View(ticket);
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiUrl = $"{GetApiBaseUrl()}/api/campus";

                ticket.CreatedDate = DateTime.Now;
                ticket.Status = string.IsNullOrWhiteSpace(ticket.Status) ? "Open" : ticket.Status;

                var response = await client.PostAsJsonAsync(apiUrl, ticket);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Service request for '{ticket.StudentName}' raised successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, $"API Error: {response.StatusCode}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error calling REST API: {ex.Message}");
            }

            return View(ticket);
        }

        // POST: /Portal/MarkResolved/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkResolved(int id)
        {
            return await UpdateStatus(id, "Resolved");
        }

        // POST: /Portal/UpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int id, string status)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiUrl = $"{GetApiBaseUrl()}/api/campus/{id}/status";

                var dto = new StatusUpdateDto { Status = status };
                var response = await client.PutAsJsonAsync(apiUrl, dto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Ticket #{id} marked as '{status}'.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"Unable to update Ticket #{id} status (Status: {response.StatusCode}).";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"API Request Exception: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Portal/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                string apiUrl = $"{GetApiBaseUrl()}/api/campus/{id}";

                var response = await client.DeleteAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = $"Ticket #{id} deleted successfully.";
                }
                else
                {
                    TempData["ErrorMessage"] = $"Failed to delete ticket #{id} (Status: {response.StatusCode}).";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"API Request Exception: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
