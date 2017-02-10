using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryController"/> class.
        /// </summary>
        public TimeEntryController()
        {
            _processor = new TimeEntryProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryController"/> class.
        /// </summary>
        /// <param name="processor">The entity processor.</param>
        public TimeEntryController(ITimeEntryProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetTimeEntries(JqGridParametersModel parameters)
        {
            // Get a list of all time entries
            var timeEntries = _processor.GetAllTimeEntries(User.Identity.Name).ToList();
            var entries =
                timeEntries.OrderByDescending(e => e.DateWorked)
                           .ThenByDescending(e => e.FromTime)
                           .Select(e => new TimeEntryModel(e))
                           .Skip(parameters.Rows*(parameters.Page - 1))
                           .Take(parameters.Rows).ToList();
            var recordCount = timeEntries.Count;

            return
                Json(
                    new JqJsonModel<TimeEntryModel>(entries)
                        {
                            CurrentPage = parameters.Page == 0 ? 1 : parameters.Page,
                            RecordCount = recordCount,
                            TotalPages =
                                recordCount%parameters.Rows == 0
                                    ? recordCount/parameters.Rows
                                    : (recordCount/parameters.Rows) + 1
                        },
                    JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProjectClientDropDown()
        {
            PopulateProjectsLookup();

            return PartialView("EditorTemplates/Project");
        }

        public ActionResult SaveTimeEntry(TimeEntryModel model, string oper, string id)
        {
            IOpResult result;

            switch (oper)
            {
                case "edit":
                    result = _processor.UpdateTimeEntry(model.ToEntity());
                    break;
                case "add":
                    result = _processor.AddTimeEntry(model.ToEntity(), User.Identity.Name);
                    break;
                case "del":
                    result = _processor.DeleteTimeEntry(int.Parse(id));
                    break;
                default:
                    result = new OpResult
                                 {
                                     IsSuccessful = false,
                                     ErrorMessage = string.Format("{0} is an unsupported operation.", oper)
                                 };
                    break;
            }

            // Return an empty string
            if (result.IsSuccessful)
            {
                return Content(string.Empty);
            }

            // Return the error message
            HttpContext.Response.StatusCode = 500;
            return Content(result.ErrorMessage);
        }

        #region Private Helpers
        /// <summary>
        /// Populates the projects lookup.
        /// </summary>
        private void PopulateProjectsLookup()
        {
            ViewBag.Projects = from p in _processor.GetProjects(User.Identity.Name)
                               where p.IsActive
                               orderby p.Client.ClientName, p.ProjectName
                               select new ProjectDropDownDisplay(p);
        }
        #endregion
    }
}
