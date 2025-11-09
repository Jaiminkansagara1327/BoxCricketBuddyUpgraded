using BoxCricketBuddy.Data;
using BoxCricketBuddy.Models;
using Microsoft.AspNetCore.Mvc;

namespace BoxCricketBuddy.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repo;
        public HomeController(IRepository repo) { _repo = repo; }

        public IActionResult Index() => View(_repo.GetAllVenues());

        public IActionResult About() => View();

        [HttpGet]
        public IActionResult Contact() => View();

        [HttpPost]
        public IActionResult Contact(ContactMessage msg)
        {
            _repo.AddContact(msg);
            TempData["ContactSuccess"] = "Thank you! Your message was received.";
            return RedirectToAction("Contact");
        }
    }
}
