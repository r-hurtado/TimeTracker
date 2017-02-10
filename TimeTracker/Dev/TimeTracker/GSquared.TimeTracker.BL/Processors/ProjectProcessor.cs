using System;
using System.Collections.Generic;
using System.Linq;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class ProjectProcessor : IProjectProcessor
    {
        private readonly ITimeTrackerRepository _db;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectProcessor"/> class.
        /// </summary>
        public ProjectProcessor()
        {
            _db = new TimeTrackerRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectProcessor"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public ProjectProcessor(ITimeTrackerRepository db)
        {
            _db = db;
        } 
        #endregion

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Project}.</returns>
        public IEnumerable<Project> GetProjects(string username)
        {
            // Get the list of clients from the database
            return
                _db.GetProjects()
                   .Where(p => p.Client.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase) || username.Equals("DBoss", StringComparison.CurrentCultureIgnoreCase))
                   .OrderBy(p => p.Client.ClientName)
                   .ThenBy(p => p.ProjectName);
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="addedProject">The added project.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult AddProject(Project addedProject)
        {
            return _db.AddProject(addedProject);
        }

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="updatedProject">The updated project.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult UpdateProject(Project updatedProject)
        {
            return _db.UpdateProject(updatedProject);
        }

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult DeleteProject(int projectId)
        {
            return _db.DeleteProject(projectId);
        }

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        public IEnumerable<Client> GetClients(string username)
        {
            return _db.GetClients().Where(c => c.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
