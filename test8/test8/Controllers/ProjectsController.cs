using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using test8.Models;

namespace test8.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjetDBContext db = new ProjetDBContext();

        // GET: Projects
        public async Task<ActionResult> Index(string id)
        {
            var projects = from p in db.Projects
                           select p;

            if (!String.IsNullOrEmpty(id))
            {
                projects = projects.Where(s => s.title.Contains(id));
            }

            return View(await projects.ToListAsync());

            //return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            Project project = new Project();
            project._users = PollDBForUsers();
            project.startDate = DateTime.Today;
            //Default to a one week project
            project.endDate = new DateTime(DateTime.Today.AddDays(7).Ticks);

            return View(project);
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,Description,leader,time,startDate,endDate,users")] Project project)
        {
            project._users = PollDBForUsers();
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            project._users = PollDBForUsers();

            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,Description,leader,time,startDate,endDate,users,_users")] Project project)
        {
            project._users = PollDBForUsers();
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        //LoadUser method for filling a dropdown
        public SelectList PollDBForUsers()
        {
            var list = new List<User>();
            int tempId = 0;

            SqlConnection conn = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=aspnet-test8-20170227094525;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT Id, UserName FROM dbo.AspNetUsers", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
                list.Add(new User { Id = ++tempId/*rdr.GetInt32(0)*/, Name = rdr.GetString(1) });

            conn.Close();

            return new SelectList(list, "Name", "Name");
        }

        public ActionResult LoadUser()
        {
            var model = new Project();
            model._users = PollDBForUsers();
            return View(model);
        }

        // GET: Projects/AddTime/5
        public ActionResult AddTime(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/AddTime/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTime([Bind(Include = "id,title,Description,leader,time,startDate,endDate")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            return View(project);
        }

        //public void AddTimeTo

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
