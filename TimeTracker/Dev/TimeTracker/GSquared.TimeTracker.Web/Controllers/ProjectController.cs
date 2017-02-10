using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly IProjectProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        public ProjectController()
        {
            _processor = new ProjectProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public ProjectController(IProjectProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            PopulateClientsLookup();

            return View();
        }

        /// <summary>
        /// Gets the clients from the database.
        /// </summary>
        public ActionResult GetProjects(JqGridParametersModel parameters)
        {
            var projects = _processor.GetProjects(User.Identity.Name).ToList();
            var models = projects.OrderBy(p => p.Client.ClientName).ThenBy(p => p.ProjectName)
                                 .Select(p => new ProjectModel(p))
                                 .Skip(parameters.Rows*(parameters.Page - 1))
                                 .Take(parameters.Rows).ToList();
            var recordCount = projects.Count;

            return
                Json(
                    new JqJsonModel<ProjectModel>(models)
                    {
                        CurrentPage = parameters.Page == 0 ? 1 : parameters.Page,
                        RecordCount = recordCount,
                        TotalPages =
                            recordCount % parameters.Rows == 0
                                ? recordCount / parameters.Rows
                                : (recordCount / parameters.Rows) + 1
                    },
                    JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetClientsDropDown()
        {
            PopulateClientsLookup();

            return PartialView("EditorTemplates/Client");
        }

        public ActionResult SaveProject(ProjectModel model, string oper, string id)
        {
            IOpResult result;

            switch (oper)
            {
                case "edit":
                    result = _processor.UpdateProject(model.ToEntity());
                    break;
                case "add":
                    result = _processor.AddProject(model.ToEntity());
                    break;
                case "del":
                    result = _processor.DeleteProject(int.Parse(id));
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

        #region Private helpers
        /// <summary>
        /// Populates the clients lookup.
        /// </summary>
        private void PopulateClientsLookup()
        {
            ViewBag.Clients = _processor.GetClients(User.Identity.Name);
        }
        #endregion
    }
}
