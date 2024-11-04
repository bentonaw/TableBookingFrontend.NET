using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using TableBookingFrontend.NET.Models;

namespace TableBookingFrontend.NET.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _client;
        private string baseUrl = "https://localhost:7043/";
        public ReservationController(HttpClient client)
        {
            _client = client;
        }

        public IActionResult Search()
        {
            return View();
        }

        // Fetches reservations by date range
        public async Task<IActionResult> SearchByDateRange(string startDate, string endDate)
        {
            var url = $"https://localhost:7043/api/Reservation/date-range?startDate={startDate}&endDate={endDate}";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error retrieving reservations.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var reservations = JsonConvert.DeserializeObject<List<ReservationVM>>(jsonResponse);

            return View("Search", reservations);
        }

        // Fetches reservations by customer last name
        public async Task<IActionResult> SearchByCustomer(string lastName)
        {
            var url = $"https://localhost:7043/api/Reservation/customer/{lastName}";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error retrieving customer reservations.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var reservations = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ReservationVM>>(jsonResponse);

            return View("Details", reservations);
        }

        // Fetches reservation by ID
        public async Task<IActionResult> SearchById(int id)
        {
            var url = $"https://localhost:7043/api/Reservation/{id}";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest("Error retrieving reservation.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var reservation = Newtonsoft.Json.JsonConvert.DeserializeObject<ReservationVM>(jsonResponse);

            return View("Details", reservation);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public async Task<IActionResult> CreateReservation()
        //{
        //    var model = new ReservationAndTimeSlotVM
        //    {
        //        reservationVM = new ReservationVM(), // Initialize reservationVM if it's not already initialized
        //        timeSlotVM = new TimeSlotVM(),
        //        timeSlotVMs = await GetTimeSlotsAsync() // Assuming GetTimeSlots() retrieves the list of time slots
        //    };
        //    ViewData["Title"] = "Create reservation";
        //    return View(model);
        //}

        //private async Task<IEnumerable<TimeSlotVM>> GetTimeSlotsAsync()
        //{
        //    var response = await _client.GetAsync($"{baseUrl}/api/Reservation/Timeslots");
        //    var json = await response.Content.ReadAsStringAsync();
        //    var timeSlots = JsonConvert.DeserializeObject<List<TimeSlotVM>>(json);
        //    if (timeSlots == null)
        //    {
        //        // Log the error or handle it as needed
        //        Console.WriteLine("Deserialization failed or no time slots returned.");
        //        return Enumerable.Empty<TimeSlotVM>();
        //    }

        //    return timeSlots;
        //}

        //private async Task<IEnumerable<TimeSlotVM>> GetTimeSlotsAsync()
        //    {
        //        var response = await _client.GetAsync($"{baseUrl}api/Reservation/Timeslots");

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            // Log the status code and response content for debugging
        //            var errorContent = await response.Content.ReadAsStringAsync();
        //            Console.WriteLine($"Error: {response.StatusCode}");
        //            Console.WriteLine($"Error Content: {errorContent}");

        //            // Optionally, you can throw an exception or handle the error as needed
        //            throw new HttpRequestException($"Request to {baseUrl}/api/Reservation/Timeslots failed with status code {response.StatusCode}");
        //        }

        //        var content = await response.Content.ReadAsStringAsync();
        //        Console.WriteLine($"Response Content: {content}"); // Debugging statement

        //        try
        //        {
        //            var timeSlots = JsonConvert.DeserializeObject<List<TimeSlotVM>>(content);
        //            if (timeSlots == null)
        //            {
        //                Console.WriteLine("Deserialization returned null.");
        //                return Enumerable.Empty<TimeSlotVM>();
        //            }

        //            return timeSlots;
        //        }
        //        catch (JsonException ex)
        //        {
        //            Console.WriteLine($"Deserialization failed: {ex.Message}");
        //            return Enumerable.Empty<TimeSlotVM>();
        //        }
        //    }

        //public IActionResult ChoosePartySize()
        //{
        //    ViewData["Title"] = "Select party size";
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ChoosePartySize(ReservationVM reservation)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(reservation);
        //    //}

        //    TempData["PartySize"] = reservation.NrOfSeats;
        //    return RedirectToAction("ChooseDate");
        //}

        //public IActionResult ChooseDate()
        //{
        //    ViewData["Title"] = "Choose date";
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult ChooseDate(ReservationVM reservation)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(reservation);
        //    //}

        //    TempData["ReservationDate"] = reservation.ReservationDate;
        //    return RedirectToAction("ChooseTimeSlot");
        //}

        //public async Task<IActionResult> ChooseTimeSlot()
        //{
        //    ViewData["Title"] = "Choose time";

        //    var response = await _client.GetAsync($"{baseUrl}api/Reservation/Timeslots");

        //    var json = await response.Content.ReadAsStringAsync();

        //    var timeSlotList = JsonConvert.DeserializeObject<List<TimeSlotVM>>(json);

        //    return View(timeSlotList);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChooseTimeSlot(ReservationVM reservation)
        //{
        //    ViewData["Title"] = "Choose timeslot";

        //    TempData["TimeSlot"] = reservation.TimeSlot;
        //    return RedirectToAction("ConfirmationScreen");
        //}

        //public IActionResult ConfirmationScreen()
        //{
        //    ViewData["Title"] = "Confirmation";
        //    return View();
        //}

        //public IActionResult ConfirmationScreen()
        //{
        //    ViewData["Title"] = "Confirmation";

        //    var reservation = new ReservationVM
        //    {
        //        NrOfSeats = (int)TempData["PartySize"],
        //        ReservationDate = (DateTime)TempData["ReservationDate"],
        //        TimeSlot = (TimeSlotVM)TempData["TimeSlot"],
        //        Customer = new CustomerVM() // Assuming you have customer data to populate
        //    };

        //    return View(reservation);
        //}

        //[HttpPost]
        //public async Task<IActionResult> ConfirmationScreen(ReservationVM reservation)
        //{
        //    //if (!ModelState.IsValid)
        //    //{
        //    //    return View(reservation);
        //    //}

        //    reservation.NrOfSeats = (int)TempData["PartySize"];
        //    reservation.ReservationDate = (DateTime)TempData["ReservationDate"];
        //    reservation.TimeSlot = (TimeSlotVM)TempData["ReservationDate"];

        //    var json = JsonConvert.SerializeObject(reservation);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _client.PostAsync($"{baseUrl}api/Reservation", content);

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> ConfirmationScreen(ReservationVM reservation)
        //{
        //    // Full ReservationVM is submitted here
        //    if (!ModelState.IsValid)
        //    {
        //        // Handle any validation issues here
        //        return View(reservation);
        //    }

        //    // Serialize the reservation object and send it to the backend
        //    var json = JsonConvert.SerializeObject(reservation);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _client.PostAsync($"{baseUrl}api/Reservation", content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        ViewData["Message"] = "thank you";
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        // Handle error
        //        return View("Error");
        //    }
        //}

    }
}
