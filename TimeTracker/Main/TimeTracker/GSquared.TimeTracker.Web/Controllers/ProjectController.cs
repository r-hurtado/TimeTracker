using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;
using Telerik.Web.Mvc;

namespace GSquared.TimeTracker.Web.Controllers
{
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
        [GridAction]
        public ActionResult GetProjects()
        {
            var projectModels = _processor.GetProjects(User.Identity.Name).Select(p => new ProjectModel(p));

            return View(new GridModel<ProjectModel>(projectModels));
        }

        [GridAction]
        public ActionResult AddProject(Project projectToAdd)
        {
            // Add the client to the database
            var result = _processor.AddProject(projectToAdd);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetProjects();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult UpdateProject(Project updatedProject)
        {
            // Update the client in the database
            var result = _processor.UpdateProject(updatedProject);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetProjects();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult DeleteProject(int deletedProjectId)
        {
            // Delete the client from the database
            var result = _processor.DeleteProject(deletedProjectId);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetProjects();
            }
            
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
