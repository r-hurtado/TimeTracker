using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Web.Models
{
    public class ProjectModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectModel"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public ProjectModel(Project entity)
        {
            ProjectId = entity.ProjectId;
            ClientId = entity.ClientId;
            ClientName = entity.Client.ClientName;
            ProjectName = entity.ProjectName;
            HourlyBillingRate = entity.HourlyBillingRate;
            BillingCode = entity.BillingCode;
            QuickbooksProjectId = entity.QuickbooksProjectId;
            IsActive = entity.IsActive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectModel"/> class.
        /// </summary>
        public ProjectModel() { }

        /// <summary>
        /// Gets or sets the project id corresponding to the <see cref="ProjectModel"/>.
        /// </summary>
        /// <value>The project id.</value>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the client id corresponding to the <see cref="ProjectModel"/>.
        /// </summary>
        /// <value>The client id.</value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        [Required]
        public string ProjectName { get; set; }

        /// <summary>
        /// Gets or sets the hourly billing rate corresponding to the <see cref="ClientModel"/>.
        /// </summary>
        /// <value>The hourly billing rate.</value>
        [Required]
        [UIHint("Currency")]
        [DefaultValue(75.0)]
        [DisplayFormat(DataFormatString = "{0:c}")]
        public decimal HourlyBillingRate { get; set; }

        /// <summary>
        /// Gets or sets the billing code corresponding to the <see cref="ProjectModel"/>.
        /// </summary>
        /// <value>The billing code.</value>
        public string BillingCode { get; set; }

        /// <summary>
        /// Gets or sets the quicken project id corresponding to the <see cref="ProjectModel"/>.
        /// </summary>
        /// <value>The quicken project id.</value>
        public string QuickbooksProjectId { get; set; }

        /// <summary>
        /// Determines whether or not the Project is Active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Converts this model to the corresponding entity
        /// </summary>
        /// <returns>Project.</returns>
        public Project ToEntity()
        {
            return new Project
                       {
                           ProjectId = ProjectId,
                           ProjectName = ProjectName,
                           BillingCode = BillingCode,
                           ClientId = ClientId,
                           HourlyBillingRate = HourlyBillingRate,
                           QuickbooksProjectId = QuickbooksProjectId,
                           IsActive = IsActive
                       };
        }
    }
}