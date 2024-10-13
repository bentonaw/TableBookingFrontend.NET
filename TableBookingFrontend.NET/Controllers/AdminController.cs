using Microsoft.AspNetCore.Mvc;

namespace TableBookingFrontend.NET.Controllers
{
	public class AdminController : Controller
	{
		private readonly HttpClient _client;
		private string baseUrl = "https://localhost:7043/";
        public AdminController(HttpClient client)
        {
            _client = client;
        }

        public IActionResult Index()
        {
            ViewData["Title"] = "Admin menu";
            //TODO
            // View reservations
            // View customers
            // view menu menu
                // add menu (collection of menuitems)
                // add menuitems
                // edit menuitems
                // quantity especially

            return View();
        }
        public async Task<IActionResult> Login()
        {
            
        }
    }
}
