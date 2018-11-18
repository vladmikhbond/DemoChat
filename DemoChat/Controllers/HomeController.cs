using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoChat.Models;
using DemoChat.Data;

namespace DemoChat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatContext _db;

        public HomeController(ChatContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            string[] ms = _db.Messages
                .Select(m => $"{m.Text}\n{m.Sign}\t{m.When}\n")
                .ToArray();
            return Content(string.Join("\n", ms));
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
