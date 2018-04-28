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
    public class HomeController : Controller
    {
        private readonly DbConnector _dbConnector;
 
        public HomeController(DbConnector connect)
        {
            _dbConnector = connect;
        }
 
        [HttpGet]
        [Route("logout")]

        public IActionResult logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [Route("login")]

        public IActionResult login(User newlogin)
        {
            var result = _dbConnector.Query($"SELECT * FROM users WHERE user_email ='{newlogin.Email}'");
            Console.WriteLine(newlogin.Email);

            if(result.Count > 0)
            {   
                if(newlogin.Password == result[0]["user_pwd"].ToString())
                {
                        //get the user_id and user_fname
                        string active_username  = result[0]["user_fname"].ToString();
                        int active_userid = Convert.ToInt32(result[0]["user_id"]);

                        //put user_id in session
                        int user_id = HttpContext.Session.GetInt32("user")?? active_userid;
                        HttpContext.Session.SetInt32("user", user_id);
                        int? id = HttpContext.Session.GetInt32("user");

                        //put username in session
                        string user_name = HttpContext.Session.GetString("username")?? active_username;
                        HttpContext.Session.SetString("username", user_name);
                        string name = HttpContext.Session.GetString("username");

                        ViewBag.ReturnActiveId = user_id;
                        ViewBag.ReturnActiveName = user_name;
                        return RedirectToAction("message", "Messages");
                }
                TempData["error"] = "Invalid email or password";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Invalid email or password";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("register")]
        public IActionResult success(User NewUSer) //Make the form input name from your views like in your models
        {             
                if(ModelState.IsValid) {
                    _dbConnector.Execute($"INSERT INTO users (user_fname, user_lname, user_email, user_pwd) VALUES ('{NewUSer.FirstName}','{NewUSer.LastName}','{NewUSer.Email}','{NewUSer.Password}');");
                    var result = _dbConnector.Query($"SELECT * FROM users WHERE user_email ='{NewUSer.Email}'");
                    
                     //get the user_id and user_fname
                    string active_username  = result[0]["user_fname"].ToString();
                    int active_userid = Convert.ToInt32(result[0]["user_id"]);

                    //put user_id in session
                    int user_id = HttpContext.Session.GetInt32("user")?? active_userid;
                    HttpContext.Session.SetInt32("user", user_id);
                    int? id = HttpContext.Session.GetInt32("user");

                    //put username in session
                    string user_name = HttpContext.Session.GetString("username")?? active_username;
                    HttpContext.Session.SetString("username", user_name);
                    string name = HttpContext.Session.GetString("username");

                    ViewBag.ReturnActiveId = user_id;
                    ViewBag.ReturnActiveName = user_name;
                    return RedirectToAction("message", "Messages");
                } 
                return View("Index");
                }
        }  
       
    }

