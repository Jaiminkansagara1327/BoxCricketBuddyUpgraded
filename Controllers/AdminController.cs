using Microsoft.AspNetCore.Mvc;
using BoxCricketBuddy.Data;

namespace BoxCricketBuddy.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repo;

        public AdminController(IRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Bookings()
        {
            var bookings = _repo is LiteDbRepository lite
                ? lite.GetAllBookings()
                : new List<BoxCricketBuddy.Models.Booking>();

            return View(bookings);
        }

        public IActionResult Messages()
        {
            var msgs = _repo is LiteDbRepository lite
                ? lite.GetAllMessages()
                : new List<BoxCricketBuddy.Models.ContactMessage>();

            return View(msgs);
        }

        public IActionResult Venues()
        {
            var venues = _repo.GetAllVenues();
            return View(venues);
        }
    }
}
