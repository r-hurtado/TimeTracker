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

            project.access = getUserAccess(project.id);

            return View(project);
        }

        public List<Project.projectAccess> getUserAccess(int id)
        {
            List<Project.projectAccess> access;

            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT [user], [Project_id], [level] FROM dbo.projectAccesses", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            access = new List<Project.projectAccess>();

            int sqlID;
            while (rdr.Read())
            {
                sqlID = rdr.GetInt32(1);
                if (sqlID == id)
                {
                    Project.projectAccess t = new Project.projectAccess(rdr.GetString(0), rdr.GetInt32(1), rdr.GetInt32(2));
                    access.Add(t);
                }
            }
            return access.OrderBy(i => i.user).ToList();
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
        [Authorize(Roles = "Admin")]
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
            project.access = getUserAccess(project.id);
            ViewBag.UserDrop = PollDBForUsers();

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

        public ActionResult Remove(int id, string user, string redirect)
        {
            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();
            string sql = "DELETE FROM [dbo].[projectAccesses] WHERE [user] = @u AND [Project_id] = @p";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", id);

            cmd.ExecuteNonQuery();

            return RedirectToAction(redirect, new { id = id });
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Add(int id)
        {
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }

            project.access = getUserAccess(id);
            SelectList users = PollDBForUsers();

            foreach (var user in users)
            {
                foreach (var access in project.access)
                    if (access.user == user.Value)
                        users = new SelectList(users.Where(x => x.Value != user.Value).ToList(), "Value", "Text");
            }

            ViewBag.Users = users;
            return View(project);
        }

        public ActionResult AddUser(int id, string user)
        {
            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();
            string sql = "INSERT INTO[dbo].[projectAccesses] ([user], [Project_id], [level]) VALUES(@u, @p, @l)";
            SqlCommand cmd = new SqlCommand(sql, conn);

            cmd.Parameters.AddWithValue("@u", user);
            cmd.Parameters.AddWithValue("@p", id);
            cmd.Parameters.AddWithValue("@l", 0);

            cmd.ExecuteNonQuery();

            return RedirectToAction("Add", new { id = id });
        }

        // GET: Projects/TimeDetails/2
        public ActionResult TimeDetails(int? id)
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
            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT [user], [time], [date], [Project_id] FROM dbo.timeLogs", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            project.inputs = new List<Project.timeLog>();

            while (rdr.Read())
                if (rdr.GetInt32(3) == id)
                {
                    Project.timeLog t = new Project.timeLog(rdr.GetString(0), rdr.GetDouble(1), rdr.GetDateTime(2));
                    project.inputs.Add(t);
                }

            project.inputs = project.inputs.OrderBy(i => i.date).ToList();

            ViewBag.Project = project.title;
            ViewBag.ID = project.id;

            return View(project.inputs.ToList());

            //return View(db.Projects.ToList());
        }

        // GET: Projects/TimeDetails/2
        public ActionResult TimePerUser(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Project project = db.Projects.Find(id);
            if (project == null)
                return HttpNotFound();

            SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT [user], [time], [date], [Project_id] FROM dbo.timeLogs", conn);
            SqlDataReader rdr = cmd.ExecuteReader();

            project.inputs = new List<Project.timeLog>();

            while (rdr.Read())
                if (rdr.GetInt32(3) == id)
                {
                    Project.timeLog t = new Project.timeLog(rdr.GetString(0), rdr.GetDouble(1), rdr.GetDateTime(2));
                    project.inputs.Add(t);
                }

            project.inputs = project.inputs.OrderBy(i => i.user).ToList();

            ViewBag.Project = project.title;
            ViewBag.ID = project.id;

            return View(project.inputs.ToList());
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
        public ActionResult AddTime([Bind(Include = "id,title,Description,leader,time,startDate,endDate,inputs")] Project project, double AddVal)
        {
            if (ModelState.IsValid)
            {
                Project.timeLog t = new Project.timeLog(User.Identity.Name, AddVal, DateTime.Now);
                project.time += AddVal;

                SqlConnection conn = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = test8.Models.ProjetDBContext; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False");
                conn.Open();
                string sql = "INSERT INTO[dbo].[timeLogs] ([user], [time], [date], [Project_id]) VALUES(@u, @t, @d, @i)";
                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.AddWithValue("@u", t.user);
                cmd.Parameters.AddWithValue("@t", t.time);
                cmd.Parameters.AddWithValue("@d", t.date);
                cmd.Parameters.AddWithValue("@i", project.id);

                cmd.ExecuteNonQuery();

                //if (project.inputs == null)
                //    project.inputs = new List<Project.timeLog>();
                //project.inputs.Add(t);
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
        [Authorize(Roles = "Admin")]
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
