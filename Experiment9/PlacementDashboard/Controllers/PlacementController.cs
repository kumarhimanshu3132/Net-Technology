using Microsoft.AspNetCore.Mvc;
using PlacementDashboard.Data;
using PlacementDashboard.Models;

namespace PlacementDashboard.Controllers
{
    public class PlacementController : Controller
    {
        private readonly IPlacementDataAccess _dataAccess;

        public PlacementController(IPlacementDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        [HttpGet]
        public IActionResult Index(string? searchTerm, string? statusFilter, string? sortBy, string? sortOrder)
        {
            var summary = _dataAccess.GetSummary();
            var placements = _dataAccess.GetAll(searchTerm, statusFilter, sortBy, sortOrder);

            var viewModel = new PlacementDashboardViewModel
            {
                Placements = placements,
                Summary = summary,
                SearchTerm = searchTerm,
                StatusFilter = statusFilter ?? "All",
                SortBy = sortBy,
                SortOrder = sortOrder
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new Placement { Status = "Placed" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Placement placement)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.Insert(placement);
                TempData["SuccessMessage"] = $"Placement record for {placement.StudentName} has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(placement);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var placement = _dataAccess.GetById(id);
            if (placement == null)
            {
                return NotFound();
            }

            return View(placement);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Placement placement)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.Update(placement);
                TempData["SuccessMessage"] = $"Placement record for {placement.StudentName} has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            return View(placement);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var placement = _dataAccess.GetById(id);
            if (placement == null)
            {
                return NotFound();
            }

            return View(placement);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var placement = _dataAccess.GetById(id);
            if (placement != null)
            {
                _dataAccess.Delete(id);
                TempData["SuccessMessage"] = $"Placement record for {placement.StudentName} was deleted.";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Report()
        {
            var reportModel = new CompanyReportViewModel
            {
                CompanySummaries = _dataAccess.GetCompanyReport(),
                OverallSummary = _dataAccess.GetSummary(),
                GeneratedAt = DateTime.Now
            };

            return View(reportModel);
        }
    }
}
