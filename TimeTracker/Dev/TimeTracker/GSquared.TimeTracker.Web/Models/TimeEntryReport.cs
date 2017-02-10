using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Web.Models
{
    /// <summary>
    /// Represents a Time Entry in report form
    /// </summary>
    public class TimeEntryReport
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryReport" /> class.
        /// </summary>
        /// <param name="data">The data for the timesheet report.</param>
        public TimeEntryReport(TimesheetReportDataByWeek data)
        {
            TotalHours = data.TotalTime.GetValueOrDefault();
            TotalRevenue = data.TotalRevenue.GetValueOrDefault();
            Week = string.Format("Week of {0}", data.Week.GetValueOrDefault().ToShortDateString());
            RevenueGoal = data.RevenueGoal;
            TimeGoal = data.TimeGoal;
        }

        /// <summary>
        /// Gets or sets the total hours corresponding to the <see cref="TimeEntryReport"/>.
        /// </summary>
        /// <value>The total hours.</value>
        public decimal TotalHours { get; set; }

        /// <summary>
        /// Gets or sets the total revenue corresponding to the <see cref="TimeEntryReport"/>.
        /// </summary>
        /// <value>The total revenue.</value>
        public decimal TotalRevenue { get; set; }

        /// <summary>
        /// Gets or sets the revenue goal corresponding to the <see cref="TimeEntryReport"/>.
        /// </summary>
        /// <value>The revenue goal.</value>
        public decimal RevenueGoal { get; set; }

        /// <summary>
        /// Gets or sets the time goal corresponding to the <see cref="TimeEntryReport"/>.
        /// </summary>
        /// <value>The time goal.</value>
        public decimal TimeGoal { get; set; }

        /// <summary>
        /// Gets or sets the week corresponding to the <see cref="TimeEntryReport"/>.
        /// </summary>
        /// <value>The week.</value>
        public string Week { get; set; }
    }
}