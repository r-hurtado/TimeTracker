using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface IProjectProcessor
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Project}.</returns>
        IEnumerable<Project> GetProjects(string username);

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="addedProject">The added project.</param>
        /// <returns>IOpResult.</returns>
        IOpResult AddProject(Project addedProject);

        /// <summary>
        /// Updates the project.
        /// </summary>
        /// <param name="updatedProject">The updated project.</param>
        /// <returns>IOpResult.</returns>
        IOpResult UpdateProject(Project updatedProject);

        /// <summary>
        /// Deletes the project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>IOpResult.</returns>
        IOpResult DeleteProject(int projectId);

        /// <summary>
        /// Gets the clients.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        IEnumerable<Client> GetClients(string username);
    }
}
