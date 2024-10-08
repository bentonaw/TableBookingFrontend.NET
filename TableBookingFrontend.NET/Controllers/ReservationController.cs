using System.Text;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateReservation()
        {
            ViewData["Title"] = "Create reservation";
            return View();
        }

        public IActionResult ChoosePartySize()
        {
            ViewData["Title"] = "Select party size";
            return View();
        }

        [HttpPost]
        public IActionResult ChoosePartySize(ReservationVM reservation)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(reservation);
            //}

            TempData["PartySize"] = reservation.NrOfSeats;
            return RedirectToAction("ChooseDate");
        }

        public IActionResult ChooseDate()
        {
            ViewData["Title"] = "Choose date";
            return View();
        }

        [HttpPost]
        public IActionResult ChooseDate(ReservationVM reservation)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(reservation);
            //}

            TempData["ReservationDate"] = reservation.ReservationDate;
            return RedirectToAction("ChooseTimeSlot");
        }

        public async Task<IActionResult> ChooseTimeSlot()
        {
            ViewData["Title"] = "Choose time";

            var response = await _client.GetAsync($"{baseUrl}api/Reservation/Timeslots");

            var json = await response.Content.ReadAsStringAsync();

            var timeSlotList = JsonConvert.DeserializeObject<List<TimeSlotVM>>(json);

            return View(timeSlotList);
        }

        [HttpPost]
        public async Task<IActionResult> ChooseTimeSlot(ReservationVM reservation)
        {
            ViewData["Title"] = "Choose timeslot";

            TempData["TimeSlot"] = reservation.TimeSlot;
            return RedirectToAction("ConfirmationScreen");
        }

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

        [HttpPost]
        public async Task<IActionResult> ConfirmationScreen(ReservationVM reservation)
        {
            // Full ReservationVM is submitted here
            if (!ModelState.IsValid)
            {
                // Handle any validation issues here
                return View(reservation);
            }

            // Serialize the reservation object and send it to the backend
            var json = JsonConvert.SerializeObject(reservation);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"{baseUrl}api/Reservation", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error
                return View("Error");
            }
        }

    }
}
