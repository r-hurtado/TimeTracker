using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Drawing;
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

        public ActionResult Report(int? id)
        {
            ViewBag.Message = "";
            ViewBag.Projects = getProjectList();
            if (id != null)
                ViewBag.ID = id;
            else
                ViewBag.ID = -2;

            return View();
        }

        public struct Proj
        {
            public int id;
            public string title;
            public DateTime start;
            public DateTime end;
        }

        public JsonResult getProjects(int id)
        {
            List<Proj> timeList = new List<Proj>();
            List<Proj> eventList = new List<Proj>();
            List<Proj> eventListComb = new List<Proj>();
            List<object> passedList = new List<object>();
            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();

            //Do loop beforehand on projects
            SqlCommand cmd = new SqlCommand("SELECT [id], [title], [startDate], [endDate] FROM dbo.Projects", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
                timeList.Add(new Proj { id = rdr.GetInt32(0), title = rdr.GetString(1), start = rdr.GetDateTime(2), end = rdr.GetDateTime(3) });

            rdr.Close();
            cmd = new SqlCommand("SELECT [user], [time], [date], [Project_id] FROM dbo.timeLogs", conn);
            rdr = cmd.ExecuteReader();

            Random randomGen = new Random();
            string[] names = { "LightSeaGreen", "DarkTurquoise", "SteelBlue", "MediumBlue", "DarkOrchid", "DarkViolet", "MediumVioletRed", "Crimson", "SaddleBrown", "DarkGoldenrod", "OliveDrab", "DarkOliveGreen", "ForestGreen", "SeaGreen", "DarkSlateBlue", "DarkRed", "Maroon", "Olive", "Green", "DarkSlateGray", "DarkCyan", "Teal", "MidnightBlue", "DarkBlue", "Navy", "Indigo", "DarkMagenta", "Purple", "DarkGreen", "Black" };

            bool addToList;
            int projId = 0;

            while (rdr.Read())
            {
                addToList = false;
                projId = 0;
                while (timeList[projId].id != rdr.GetInt32(3))
                    projId++;
                Proj temp = timeList[projId];

                //Switch for report type
                //{
                //Pair to set days worked on
                if (temp.id == id)
                    addToList = true;
                else if (id == -1)
                {
                    if (temp.end > DateTime.Now)
                        addToList = true;
                }
                else if (id == -2)
                    addToList = true;
                //}

                //Add to list
                if (addToList)
                    eventList.Add(new Proj { id = temp.id, title = temp.title + "\n - " + rdr.GetString(0).Split('@')[0] + ": ", start = rdr.GetDateTime(2), end = rdr.GetDateTime(2).AddHours(rdr.GetDouble(1)) }); //allDay = true, color = names[temp.id % names.Count()] });
            }
            int index;
            foreach (Proj item in eventList)
            {
                index = eventListComb.FindIndex(i => i.id == item.id && i.start.ToShortDateString() == item.start.ToShortDateString());//inner.title = inner.title + " - " + inner.title.Split('-')[0] + ": ";

                if (index >= 0)
                {
                    Proj temp = eventListComb[index];
                    TimeSpan time = item.end.Subtract(item.start);
                    temp.end = temp.end.AddHours(time.Hours);
                    temp.end = temp.end.AddMinutes(time.Minutes);
                    eventListComb.RemoveAt(index);
                    eventListComb.Add(temp);
                }
                else
                {
                    Proj temp = item;
                    temp.title = item.title.Split('-')[0] + " - (Total): ";
                    eventListComb.Add(temp);
                }
            }

            foreach (Proj item in eventList)
            /*if (!eventListComb.Contains(item))
                eventListComb.Add(item);*/
            {
                string itemStr = item.title.Split('\n')[1].Split(' ')[2].Split(':')[0];
                index = eventListComb.FindIndex(i => i.title.Split(' ')[0] == itemStr && i.start.ToShortDateString() == item.start.ToShortDateString());//inner.title = inner.title + " - " + inner.title.Split('-')[0] + ": ";

                if (index >= 0)
                {
                    Proj temp = eventListComb[index];
                    TimeSpan time = item.end.Subtract(item.start);
                    temp.end = temp.end.AddHours(time.Hours);
                    temp.end = temp.end.AddMinutes(time.Minutes);
                    eventListComb.RemoveAt(index);
                    eventListComb.Add(temp);
                }
                else
                {
                    Proj temp = item;
                    temp.id = 0;
                    temp.title = itemStr + " - (Total): ";
                    eventListComb.Add(temp);
                }
            }

            foreach (Proj item in eventListComb)
                passedList.Add(new { id = item.id, title = item.title + (item.end.Subtract(item.start).TotalMinutes - item.end.Subtract(item.start).TotalMinutes % 15) / 60, start = item.start, allDay = true, color = names[item.id == 0 ? 0 : (item.id % (names.Count() - 1) + 1)] });

            return Json(passedList.ToArray(), JsonRequestBehavior.AllowGet);
        }

        public struct projItem
        {
            public int id;
            public string title;
        }

        public List<projItem> getProjectList()
        {
            List<projItem> retList = new List<projItem>();
            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT [id], [title] FROM dbo.Projects", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
                retList.Add(new projItem { id = rdr.GetInt32(0), title = rdr.GetString(1) });

            return retList;
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

        public ActionResult mkAdmin(test8.Models.ApplicationUser name)
        {
            /*var context = new ApplicationDbContext();
            var users = context.Users.ToList();
            foreach( var user in users)
            {
                if (user.Id == id)
                    user.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { UserId = id, RoleId = "1e6954ee-8066-48ca-9f9b-66cf2b305979" });
            }*/
            //name.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole() { UserId = name.Id, RoleId = "1e6954ee-8066-48ca-9f9b-66cf2b305979" });
            

            return RedirectToAction("Admin");
        }
    }
}