using Microsoft.AspNetCore.Mvc;
using BoxCricketBuddy.Data;

namespace BoxCricketBuddy.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRepository _repo;

        // Hardcoded admin credentials (good for project demo)
        private const string ADMIN_EMAIL = "admin@boxcricket.com";
        private const string ADMIN_PASSWORD = "12345";

        public AdminController(IRepository repo)
        {
            _repo = repo;
        }

        private bool IsLoggedIn()
        {
            return HttpContext.Session.GetString("AdminLoggedIn") == "true";
        }

        private IActionResult Protect()
        {
            if (!IsLoggedIn())
                return Redirect("/Admin/Login");

            return null; // meaning OK
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (email == ADMIN_EMAIL && password == ADMIN_PASSWORD)
            {
                HttpContext.Session.SetString("AdminLoggedIn", "true");
                return Redirect("/Admin");
            }

            TempData["Error"] = "Invalid login details.";
            return Redirect("/Admin/Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLoggedIn");
            return Redirect("/Admin/Login");
        }


        public IActionResult Index()
        {
            var guard = Protect();
            if (guard != null) return guard;

            return View();
        }

        public IActionResult Bookings()
        {
            var guard = Protect();
            if (guard != null) return guard;

            var data = (_repo as LiteDbRepository)?.GetAllBookings() ?? new List<BoxCricketBuddy.Models.Booking>();
            return View(data);
        }

        public IActionResult Messages()
        {
            var guard = Protect();
            if (guard != null) return guard;

            var msgs = (_repo as LiteDbRepository)?.GetAllMessages() ?? new List<BoxCricketBuddy.Models.ContactMessage>();
            return View(msgs);
        }

        public IActionResult Venues()
        {
            var guard = Protect();
            if (guard != null) return guard;

            var venues = _repo.GetAllVenues();
            return View(venues);
        }
    }
}
