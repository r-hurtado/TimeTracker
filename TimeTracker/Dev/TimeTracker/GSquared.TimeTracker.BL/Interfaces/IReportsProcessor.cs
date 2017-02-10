using System;
using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface IReportsProcessor
    {
        /// <summary>
        /// Gets the total hours by week for the given date range
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{TimesheetReportDataByWeek}.</returns>
        IEnumerable<TimesheetReportDataByWeek> GetTimesheetReportDataByWeek(DateTime startDate, DateTime endDate, string username);

        /// <summary>
        /// Gets the monthly time entries.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="fromDate">The From Date</param>
        /// <param name="toDate">The To Date</param>
        /// <returns>IEnumerable{TimeEntry}.</returns>
        IEnumerable<TimeEntry> GetMonthlyTimeEntries(string user, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>User.</returns>
        User GetUser(string user);
    }
}