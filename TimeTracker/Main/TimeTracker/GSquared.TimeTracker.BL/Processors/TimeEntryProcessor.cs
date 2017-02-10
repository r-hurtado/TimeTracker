using System;
using System.Collections.Generic;
using System.Linq;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Properties;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class TimeEntryProcessor : ITimeEntryProcessor
    {
        private readonly ITimeTrackerRepository _db;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryProcessor"/> class.
        /// </summary>
        public TimeEntryProcessor()
        {
            _db = new TimeTrackerRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryProcessor"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public TimeEntryProcessor(ITimeTrackerRepository db)
        {
            _db = db;
        } 
        #endregion

        /// <summary>
        /// Gets all time entries for the supplied user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{TimeEntry}.</returns>
        public IEnumerable<TimeEntry> GetAllTimeEntries(string username)
        {
            return
                _db.GetAllTimeEntries()
                   .Where(te => te.CreatedBy.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the total hours by week for the given date range
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{TimesheetReportDataByWeek}.</returns>
        public IEnumerable<TimesheetReportDataByWeek> GetTimesheetReportDataByWeek(DateTime startDate, DateTime endDate, string username)
        {
            return
                _db.GetTimesheetReportDataByWeek(startDate, endDate, username);
        }

        /// <summary>
        /// Gets all of the projects the given user can access.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Project}.</returns>
        public IEnumerable<Project> GetProjects(string username)
        {
            return
                _db.GetProjects()
                   .Where(pr => pr.Client.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Adds the time entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="username">The username.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult AddTimeEntry(TimeEntry entry, string username)
        {
            // Get the User Id
            var userEntity = _db.GetUser(username);
            if (userEntity == null)
            {
                throw new ArgumentException(Resources.InvalidUserIdMessage);
            }
            entry.UserId = userEntity.UserId;
            entry.CreatedBy = username;
            entry.CreatedAt = DateTime.Now;
            return _db.AddTimeEntry(entry, username);
        }

        /// <summary>
        /// Updates the time entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult UpdateTimeEntry(TimeEntry entry)
        {
            return _db.UpdateTimeEntry(entry);
        }

        /// <summary>
        /// Deletes the time entry.
        /// </summary>
        /// <param name="entryId">The entry id.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult DeleteTimeEntry(int entryId)
        {
            return _db.DeleteTimeEntry(entryId);
        }
    }
}
