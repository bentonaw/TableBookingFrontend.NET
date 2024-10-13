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
        // Admin Index Page (accessible only if the correct cookie is present)
        [HttpGet]
        public IActionResult Index()
        {
            string? cookieValue = Request.Cookies["CookieKey"];
            string? anotherCookieValue = Request.Cookies["30CookieKey"];
            string adminCookieValue = "isAdminCookie";
            if (cookieValue == adminCookieValue || anotherCookieValue == adminCookieValue){
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

        public IActionResult Menu()
        {
            return View();
        }
        public IActionResult Customers()
        {
            return View();
        }
        public IActionResult Reservations()
        {
            return View();
        }
        public IActionResult Settings()
        {
            return View();
        }
    }
}
