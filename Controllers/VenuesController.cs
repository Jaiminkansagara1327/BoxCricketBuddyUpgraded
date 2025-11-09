using BoxCricketBuddy.Data;
using BoxCricketBuddy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxCricketBuddy.Controllers
{
    public class VenuesController : Controller
    {
        private readonly IRepository _repo;
        public VenuesController(IRepository repo) { _repo = repo; }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var venue = _repo.GetVenue(id);
            if (venue == null) return NotFound();
            return View(venue);
        }

        [HttpGet]
        public IActionResult Search(string city = "Rajkot", string? pitchType = null, int? maxPrice = null)
        {
            var results = _repo.Search(city, pitchType, maxPrice);
            return PartialView("_VenueCards", results);
        }

        [HttpPost]
        public IActionResult Book([FromForm] Booking booking)
        {
            booking.Reference = "BC" + System.DateTime.Now.ToString("yyyyMMddHHmmss") + new System.Random().Next(100,999);
            _repo.AddBooking(booking);
            return Json(new { success = true, reference = booking.Reference });
        }
    }
}
