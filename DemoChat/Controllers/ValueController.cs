using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoChat.Data;
using DemoChat.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoChat.Controllers
{
    [EnableCors("AllowAll")]
    public class ValueController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ValueController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET api/messages
        [HttpGet("api/messages")]
        public IActionResult Get()
        {
            return Json(_db.Messages);
        }

        // GET api/messages/5
        [HttpGet("api/messages/{id}")]
        public IActionResult Get(int id)
        {
            var message = _db.Messages.SingleOrDefault(m => m.Id == id);
            if (message == null)
            {
                return NotFound();
            }
            return Json(message);
        }

        // POST api/messages
        [HttpPost("api/messages")]
        public IActionResult Post(string text, string userName)
        {
            var user = _db.Users.SingleOrDefault(u => u.UserName == userName);
            if (user == null)
                return NotFound();
            var newMes = new Message { Text = text, User = user, When = DateTime.Now };
            _db.Messages.Add(newMes);
            _db.SaveChanges();
            return Ok();
        }

    }

}
