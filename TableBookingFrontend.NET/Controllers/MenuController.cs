using Microsoft.AspNetCore.Mvc;

namespace TableBookingFrontend.NET.Controllers
{
    public class MenuController : Controller
    {
        private readonly HttpClient _client;
        private string baseUrl = "https://localhost:7043/";
        public MenuController(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
