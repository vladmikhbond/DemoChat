using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DemoChat.Models;
using Microsoft.AspNetCore.Authorization;
using DemoChat.Data;
using Microsoft.EntityFrameworkCore;

namespace DemoChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            // get messages with users
            ViewBag.Messages = _db.Messages.Include(m => m.User);
            return View();
        }

        [HttpPost]
        public IActionResult Index(Message mes)
        {
            // find current user in db
            var user = _db.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
            if (user == null)
                return NotFound();

            // complete the message
            mes.When = DateTime.Now;
            mes.User = user;

            // save the message
            _db.Messages.Add(mes);
            _db.SaveChanges();

            ViewBag.Messages = _db.Messages;
            return View();
        }

        //[Authorize(Policy = "ForBossOnly")]
        [Authorize(Roles = "boss,admin")]
        public IActionResult About()
        {
            ViewData["Message"] = "Top secret data for boss only.";

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
