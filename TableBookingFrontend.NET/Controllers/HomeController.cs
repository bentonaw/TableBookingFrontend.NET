using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TableBookingFrontend.NET.Models;

namespace TableBookingFrontend.NET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //ViewData["Message"] = "Hi " + name;

   //         var sessionCookie = new CookieOptions
   //         {
   //             Path = "/"
   //         };

   //         Response.Cookies.Append("CookieKey", "CookieValue", sessionCookie);

   //         var persistentCookie = new CookieOptions
   //         {
   //             Path = "/",
   //             Expires = DateTimeOffset.Now.AddDays(30),
   //             //HttpOnly = true,
   //             //Secure = true,
   //             //SameSite = SameSiteMode.Strict
   //         };

			//Response.Cookies.Append("PersistentCookie", "From home", persistentCookie);

   //         string? cookieValue = Request.Cookies["CookieKey"];

			return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorVM { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
