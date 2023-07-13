﻿using Microsoft.AspNetCore.Mvc;
using DailyPlanner.Models;

namespace DailyPlanner.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(User user)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("User has been authentificated");
                Console.WriteLine(
                    $"Login: {user.Login}, " +
                    $"Password: {user.Password}");

                return Redirect("Main");
            }

            return View();
        }
    }
}
