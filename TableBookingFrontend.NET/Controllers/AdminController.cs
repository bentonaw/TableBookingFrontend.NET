using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TableBookingFrontend.NET.Models;

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
        // Admin Index Page (accessible only if the correct cookie is present)
        [HttpGet]
        public IActionResult Index()
        {
            string? cookieValue = Request.Cookies["CookieKey"];
            string? anotherCookieValue = Request.Cookies["30CookieKey"];
            string adminCookieValue = "isAdminCookie";
            if (cookieValue == adminCookieValue || anotherCookieValue == adminCookieValue){
                ViewData["Title"] = "Admin menu";

                return View();
            }
            return RedirectToAction("Login");
        }

        // Login Page
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login submission
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            bool isValidUser = await ValidateUser(email, password);

            if (isValidUser)
            {
                if (rememberMe)
                {
                    // Set a persistent cookie for 8 hours if login is successful
                    var persistentCookie = new CookieOptions
                    {
                        Path = "/",
                        Expires = DateTimeOffset.Now.AddDays(30),
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                    };

                    Response.Cookies.Append("30CookieKey", "isAdminCookie", persistentCookie);
                }
                
                else
                {
                    // Set a session cookie (will be cleared when the browser closes)
                    var sessionCookie = new CookieOptions
                    {
                        Path = "/",
                        HttpOnly = true,
                        Secure = false,
                        SameSite = SameSiteMode.Strict,
                        Expires = null // Null expiration means it's a session cookie
                    };

                    Response.Cookies.Append("CookieKey", "isAdminCookie", sessionCookie);
                }

                // Redirect to the Admin page after successful login
                return RedirectToAction("Index");
            }

            // If login fails, return back to the Login page with an error message
            ViewBag.ErrorMessage = "Invalid email or password";
            return View();

        }

        private async Task<bool> ValidateUser(string email, string password)
        {
            // This is a placeholder function. TODO Replace with real logic to check with the database.
            if (email == "admin@snacksnotreal.com" && password == "admin123")
            {
                return true;
            }

            return false;
        }

        // POST: Logout action
        [HttpPost]
        public IActionResult Logout()
        {
            // Delete the cookie by setting its expiration to a past date
            Response.Cookies.Delete("CookieKey");
            Response.Cookies.Delete("30CookieKey");

            // Redirect to the login page
            return RedirectToAction("Login");
        }

        // CRUD operations for Reservations
        public async Task<IActionResult> GetReservations()
        {
            ViewData["Title"] = "Reservations";

            var response = await _client.GetAsync($"{baseUrl}api/Reservation");

            var json = await response.Content.ReadAsStringAsync();

            var reservationList = JsonConvert.DeserializeObject<List<ReservationVM>>(json);

            return View(reservationList);
        }

        //[HttpGet]
        //public IActionResult CreateReservation()
        //{
        //    ViewData["Title"] = "Create Reservation";
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateReservation(ReservationVM reservation)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(reservation);
        //    }

        //    var json = JsonConvert.SerializeObject(reservation);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _client.PostAsync($"{baseUrl}api/Reservation", content);

        //    return RedirectToAction("Reservations");
        //}

        [HttpGet]
        public async Task<IActionResult> EditReservation(int id)
        {
            var response = await _client.GetAsync($"{baseUrl}api/Reservation/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var reservation = JsonConvert.DeserializeObject<ReservationVM>(json);

            return View(reservation);
        }

        [HttpPost]
        public async Task<IActionResult> EditReservation(ReservationVM reservation)
        {
            if (!ModelState.IsValid)
            {
                return View(reservation);
            }

            var json = JsonConvert.SerializeObject(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{baseUrl}api/Reservation/{reservation.ReservationId}", content);

            return RedirectToAction("Reservations");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}api/Reservation/{id}");

            return RedirectToAction("Reservations");
        }

        // CRUD operations for Menu Items
        public async Task<IActionResult> Menu()
        {
            ViewData["Title"] = "Menu";

            var response = await _client.GetAsync($"{baseUrl}api/Menu/menu-items");

            var json = await response.Content.ReadAsStringAsync();

            var menuList = JsonConvert.DeserializeObject<List<MenuItemVM>>(json);

            return View(menuList);
        }

        [HttpGet]
        public IActionResult CreateMenuItem()
        {
            ViewData["Title"] = "Create Menu Item";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMenuItem(MenuItemVM menuItem)
        {
            if (!ModelState.IsValid)
            {
                return View(menuItem);
            }

            var json = JsonConvert.SerializeObject(menuItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUrl}api/Menu/menu-items", content);

            return RedirectToAction("Menu");
        }

        [HttpGet]
        public async Task<IActionResult> EditMenuItem(int id)
        {
            var response = await _client.GetAsync($"{baseUrl}api/Menu/menu-items/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var menuItem = JsonConvert.DeserializeObject<MenuItemVM>(json);

            return View(menuItem);
        }

        [HttpPost]
        public async Task<IActionResult> EditMenuItem(MenuItemVM menuItem)
        {
            if (!ModelState.IsValid)
            {
                return View(menuItem);
            }

            var json = JsonConvert.SerializeObject(menuItem);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{baseUrl}api/Menu/menu-items/{menuItem.MenuItemId}", content);

            return RedirectToAction("Menu");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}api/Menu/menu-items/{id}");

            return RedirectToAction("Menu");
        }

        // CRUD operations for Customers
        public async Task<IActionResult> Customers()
        {
            ViewData["Title"] = "Customers";

            var response = await _client.GetAsync($"{baseUrl}api/Customer");

            var json = await response.Content.ReadAsStringAsync();

            var customerList = JsonConvert.DeserializeObject<List<CustomerVM>>(json);

            return View(customerList);
        }

        [HttpGet]
        public IActionResult CreateCustomer()
        {
            ViewData["Title"] = "New Customer";
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerVM customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUrl}api/Customer", content);

            return RedirectToAction("Customers");
        }

        [HttpGet]
        public async Task<IActionResult> EditCustomer(int id)
        {
            var response = await _client.GetAsync($"{baseUrl}api/Customer/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<CustomerVM>(json);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> EditCustomer(CustomerVM customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }

            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{baseUrl}api/Customer/{customer.CustomerId}", content);

            return RedirectToAction("Customers");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}api/Customer/{id}");

            return RedirectToAction("Customers");

        }

        public IActionResult Settings()
        {
            return View();
        }
    }
}
