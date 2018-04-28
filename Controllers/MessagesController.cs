using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using wall.Models;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace wall.Controllers
{
    public class MessagesController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public MessagesController(DbConnector connect)
        {
            _dbConnector = connect;
        }
 
        [HttpPost]
        [Route("wall")]

        public IActionResult addmessage(string message)
        {
            //get user_id again from the HomeController
             int? id = HttpContext.Session.GetInt32("user");
             ViewBag.ReturnUserId = id;

             //Insert DB
            _dbConnector.Execute($"INSERT INTO messages (message, user_id) VALUES ('{message}','{id}');");
            return RedirectToAction("message");
        }

        [HttpGet]
        [Route("message")]
        public IActionResult message()
        {
             string name = HttpContext.Session.GetString("username");
             ViewBag.ReturnActiveName = name;

             int? id = HttpContext.Session.GetInt32("user");
             ViewBag.ReturnUserId = id;

            //Display messages and comments
            var display_messages = _dbConnector.Query($"SELECT messages.*, user_fname AS name, mcreated_at as date  FROM users JOIN messages ON users.user_id = messages.user_id ORDER BY date DESC;");
            var display_comments = _dbConnector.Query($"SELECT comments.*, user_fname AS comment_name, ccreated_at AS comment_date FROM users JOIN comments   ON users.user_id = comments.user_id;");
            ViewBag.MessageDisplay = display_messages;
            ViewBag.CommentDisplay = display_comments;
            return View("wall");
        }
    }
}
