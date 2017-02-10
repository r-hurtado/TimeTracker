using System;
using System.ComponentModel.DataAnnotations;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Web.Models
{
    /// <summary>
    /// Represents a time entry
    /// </summary>
    public class TimeEntryModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryModel"/> class.
        /// </summary>
        public TimeEntryModel() 
        {
            IsBillable = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryModel"/> class.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public TimeEntryModel(TimeEntry entity)
        {
            TimeEntryId = entity.TimeEntryId;
            ProjectId = entity.ProjectId;
            ProjectDisplayName = string.Format("{0} - {1}", entity.Project.Client.ClientName, entity.Project.ProjectName);
            FromTime = entity.FromTime.ToShortTimeString();
            ToTime = entity.ToTime.HasValue ? entity.ToTime.Value.ToShortTimeString() : string.Empty;
            TotalTime = entity.TotalTime.ToString("#0.00");
            DateWorked = entity.DateWorked.ToShortDateString();
            IsBillable = entity.IsBillable;
            Description = entity.Description;
        }

        /// <summary>
        /// Converts this <see cref="TimeEntryModel"/> to a <see cref="TimeEntry"/>
        /// </summary>
        /// <returns>
        /// A <see cref="TimeEntry"/> object representing this instance of a <see cref="TimeEntryModel"/> object
        /// </returns>
        public TimeEntry ToEntity()
        {
            var entity = new TimeEntry
            {
                TimeEntryId = TimeEntryId,
                ProjectId = ProjectId,
                FromTime = String.IsNullOrWhiteSpace(FromTime) ? DateTime.Now : DateTime.Parse(DateWorked + " " + FromTime),
                ToTime = String.IsNullOrWhiteSpace(ToTime) ? null as DateTime? : DateTime.Parse(DateWorked + " " + ToTime),
                TotalTime = decimal.Parse(TotalTime),
                DateWorked = DateTime.Parse(DateWorked),
                IsBillable = IsBillable,
                Description = Description
            };

            return entity;
        }

        /// <summary>
        /// Gets or sets the time entry id corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The time entry id.</value>
        public int TimeEntryId { get; set; }

        /// <summary>
        /// Gets or sets the project id.
        /// </summary>
        /// <value>The project id.</value>
        [UIHint("Project")]
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets from time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>From time.</value>
        [UIHint("Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public string FromTime { get; set; }

        /// <summary>
        /// Gets or sets to time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>To time.</value>
        [UIHint("Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public string ToTime { get; set; }

        /// <summary>
        /// Gets or sets the total time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The total time.</value>
        [DisplayFormat(DataFormatString="{0:###.##}")]
        public string TotalTime { get; set; }

        /// <summary>
        /// Gets or sets the date worked corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The date worked.</value>
        [UIHint("Date")]
        [DisplayFormat(DataFormatString="{0:d}")]
        public string DateWorked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is billable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is billable; otherwise, <c>false</c>.
        /// </value>
        public bool IsBillable { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        /// <value>The name of the project.</value>
        public string ProjectDisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }
    }
}