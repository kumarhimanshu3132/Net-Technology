using Microsoft.AspNetCore.Mvc;
using FeedbackManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
namespace FeedbackManagementApp.Controllers
{
    public class FeedbackController : Controller
    {
        private static List<Feedback> _feedbacks = new List<Feedback>();
        [HttpGet]
        public IActionResult Index()
        {
            return View(_feedbacks);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Feedback model)
        {
            if (ModelState.IsValid)
            {
                model.Id = _feedbacks.Any() ? _feedbacks.Max(f => f.Id) + 1 : 1;
                model.SubmittedAt = DateTime.Now;
                _feedbacks.Add(model);

                TempData["SuccessMessage"] = "Thank you! Your feedback has been recorded successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}