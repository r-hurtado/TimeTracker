using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using test8.Models;

namespace test8.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Project()
        {
            return View();
        }

        public ActionResult Notifications()
        {
            ViewBag.Message = "Notification page.";

            return View();
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Report page.";

            return View();
        }

        public ActionResult Data()
        {
            ViewBag.Message = "Data page.";

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            ViewBag.Message = "Admin page.";
            var context = new ApplicationDbContext();
            var users = context.Users.ToList();

            return View(users);
        }
    }
}