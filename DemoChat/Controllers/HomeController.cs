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
            ViewBag.Messages = _db.Messages;
            return View(new Message());
        }

        [HttpPost]
        public IActionResult Index(Message mes)
        {
            mes.When = DateTime.Now;
            _db.Messages.Add(mes);
            ViewBag.Messages = _db.Messages;
            return View();
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
