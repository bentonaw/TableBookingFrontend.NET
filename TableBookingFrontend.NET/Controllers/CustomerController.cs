using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TableBookingFrontend.NET.Models;

namespace TableBookingFrontend.NET.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;
        private string baseUrl = "https://localhost:7043/";
        public CustomerController(HttpClient client)
        {
            _client = client;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["Title"] = "Customers";

            var response = await _client.GetAsync($"{baseUrl}api/Customer");

            var json = await response.Content.ReadAsStringAsync();

            var customerList = JsonConvert.DeserializeObject<List<CustomerVM>>(json);

            return View(customerList);
        }

        public IActionResult Create()
        {
            ViewData["Title"] = "New Customer";

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CustomerVM customer)
        {
            var json = JsonConvert.SerializeObject(customer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{baseUrl}api/Customer", content);

            return RedirectToAction("Index");
        }
    }
}
