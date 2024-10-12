using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TableBookingFrontend.NET.Models;

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
        public async Task<IActionResult> Index()
        {
			ViewData["Title"] = "Menu";

			var response = await _client.GetAsync($"{baseUrl}api/Menu");

			var json = await response.Content.ReadAsStringAsync();

			var customerList = JsonConvert.DeserializeObject<List<MenuItemVM>>(json);

			return View(customerList);
		}
    }
}
