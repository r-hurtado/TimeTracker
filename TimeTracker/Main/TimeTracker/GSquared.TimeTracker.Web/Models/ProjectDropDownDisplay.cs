using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Web.Models
{
    /// <summary>
    /// Provides a class for the Drop-down
    /// </summary>
    public class ProjectDropDownDisplay
    {
        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the display name of the project.
        /// </summary>
        /// <value>The display name of the project.</value>
        public string ProjectDisplayName { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectDropDownDisplay"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public ProjectDropDownDisplay(Project entity)
        {
            ProjectId = entity.ProjectId;
            ProjectDisplayName = string.Format("{0} - {1}", entity.Client.ClientName, entity.ProjectName);
        }
    }
}