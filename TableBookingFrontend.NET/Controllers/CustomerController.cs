﻿using System.Text;
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
			if (!ModelState.IsValid)
			{
				return View(customer);
			}

			var json = JsonConvert.SerializeObject(customer);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync($"{baseUrl}api/Customer", content);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"{baseUrl}api/Customer/{id}");

            var json = await response.Content.ReadAsStringAsync();

            var customer = JsonConvert.DeserializeObject<CustomerVM>(json);

            return View(customer);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CustomerVM customer)
        {
            if (!ModelState.IsValid)
            {
                return View(customer);
            }
            var json = JsonConvert.SerializeObject(customer);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _client.PatchAsync($"{baseUrl}api/Customer/{customer.CustomerId}", content);

            if (!response.IsSuccessStatusCode)
            {
                // Handle error response
                return StatusCode((int)response.StatusCode, "An error occurred while updating the customer.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.DeleteAsync($"{baseUrl}api/Customer/{id}");

            return RedirectToAction("Index");
        }
    }
}
