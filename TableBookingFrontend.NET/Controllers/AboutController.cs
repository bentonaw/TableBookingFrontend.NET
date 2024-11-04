using Microsoft.AspNetCore.Mvc;

namespace TableBookingFrontend.NET.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
