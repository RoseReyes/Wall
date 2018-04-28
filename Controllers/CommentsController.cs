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
    public class CommentsController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public CommentsController(DbConnector connect)
        {
            _dbConnector = connect;
        }

        [HttpPost]
        [Route("comments")]

        public IActionResult addcomments(int messageid, string comments)
        {
            //get user_id again from the HomeController
            int? id = HttpContext.Session.GetInt32("user");
            ViewBag.ReturnUserId = id;

            //Insert DB
            _dbConnector.Execute($"INSERT INTO comments (comment, message_id, user_id) VALUES ('{comments}','{messageid}','{id}');");
            return RedirectToAction("message","Messages");
        }
    }
}
