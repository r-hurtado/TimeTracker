using System;
using System.Collections.Generic;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class ReportsProcessor : IReportsProcessor
    {
        private readonly ITimeTrackerRepository _db;

        public ReportsProcessor()
        {
            _db = new TimeTrackerRepository();
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
        /// Gets the monthly time entries.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="fromDate">The From Date</param>
        /// <param name="toDate">The To Date</param>
        /// <returns>IEnumerable{TimeEntry}.</returns>
        public IEnumerable<TimeEntry> GetMonthlyTimeEntries(string user, DateTime fromDate, DateTime toDate)
        {
            return _db.GetMonthlyTimeEntries(user, fromDate, toDate);
        } 

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>User.</returns>
        public User GetUser(string user)
        {
            return _db.GetUser(user);
        }
    }
}
