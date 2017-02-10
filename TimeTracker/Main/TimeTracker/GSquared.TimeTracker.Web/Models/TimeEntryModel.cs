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
            UserId = entity.UserId;
            ProjectId = entity.ProjectId;
            ProjectDisplayName = string.Format("{0} - {1}", entity.Project.Client.ClientName, entity.Project.ProjectName);
            FromTime = entity.FromTime;
            ToTime = entity.ToTime;
            TotalTime = entity.TotalTime;
            DateWorked = entity.DateWorked;
            IsBillable = entity.IsBillable;
            CreatedAt = entity.CreatedAt;
            CreatedBy = entity.CreatedBy;
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
                UserId = UserId,
                FromTime = FromTime.HasValue ? DateTime.Parse(DateWorked.ToShortDateString() + " " + FromTime.Value.ToShortTimeString()) : null as DateTime?,
                ToTime = ToTime.HasValue ? DateTime.Parse(DateWorked.ToShortDateString() + " " + ToTime.Value.ToShortTimeString()) : null as DateTime?,
                TotalTime = TotalTime,
                DateWorked = DateWorked,
                IsBillable = IsBillable,
                CreatedAt = CreatedAt.GetValueOrDefault(),
                CreatedBy = CreatedBy,
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
        /// Gets or sets the projec id.
        /// </summary>
        /// <value>The projec id.</value>
        [UIHint("Project")]
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets or sets the user id corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The user id.</value>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets from time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>From time.</value>
        [UIHint("Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? FromTime { get; set; }

        /// <summary>
        /// Gets or sets to time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>To time.</value>
        [UIHint("Time")]
        [DisplayFormat(DataFormatString = "{0:t}")]
        public DateTime? ToTime { get; set; }

        /// <summary>
        /// Gets or sets the total time corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The total time.</value>
        [DisplayFormat(DataFormatString="{0:###.##}")]
        public decimal TotalTime { get; set; }

        /// <summary>
        /// Gets or sets the date worked corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The date worked.</value>
        [UIHint("Date")]
        [DisplayFormat(DataFormatString="{0:d}")]
        public DateTime DateWorked { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is billable.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is billable; otherwise, <c>false</c>.
        /// </value>
        public bool IsBillable { get; set; }

        /// <summary>
        /// Gets or sets the created at corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The created at.</value>
        public DateTime? CreatedAt { get; private set; }

        /// <summary>
        /// Gets or sets the created by corresponding to the <see cref="TimeEntryModel"/>.
        /// </summary>
        /// <value>The created by.</value>
        public string CreatedBy { get; private set; }

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