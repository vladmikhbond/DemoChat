using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoChat.Data;
using DemoChat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoChat.Controllers
{
    //[EnableCors("AllowAll")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ValueController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ValueController(ApplicationDbContext db)
        {
            _db = db;
        }


        // GET api/messages
        [AllowAnonymous]
        [HttpOptions("api/messages")]
        public void Options()
        {
            Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept, Authorization");
            //Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
            //Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
            Response.ContentType = "text/html";
        }

        // GET api/messages
        [HttpGet("api/messages")]
        public IActionResult Get()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "*");
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
