using System;
using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Web.Models;
using Telerik.Web.Mvc;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
    public class TimeEntryController : Controller
    {
        private readonly ITimeEntryProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryController"/> class.
        /// </summary>
        public TimeEntryController()
        {
            _processor = new TimeEntryProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeEntryController"/> class.
        /// </summary>
        /// <param name="processor">The entity processor.</param>
        public TimeEntryController(ITimeEntryProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            PopulateProjectsLookup();

            return View();
        }

        [GridAction]
        public ActionResult GetTimeEntries()
        {
            // Get a list of all time entries
            var entries = (from e in _processor.GetAllTimeEntries(User.Identity.Name)
                           orderby e.DateWorked descending, e.FromTime descending
                           where e.DateWorked >= DateTime.Now.AddDays(-45)
                                && e.CreatedBy.Equals(User.Identity.Name, StringComparison.CurrentCultureIgnoreCase)
                          select new TimeEntryModel(e)).ToList<TimeEntryModel>();
                         
            return View(new GridModel<TimeEntryModel>(entries));
        }

        [GridAction]
        public ActionResult AddTimeEntry(TimeEntryModel entryToAdd)
        {
            // Add the time entry to the database
            var result = _processor.AddTimeEntry(entryToAdd.ToEntity(), User.Identity.Name);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetTimeEntries();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult UpdateTimeEntry(TimeEntryModel updatedEntry)
        {
            // Update the time entry in the database
            var result = _processor.UpdateTimeEntry(updatedEntry.ToEntity());

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetTimeEntries();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult DeleteTimeEntry(int id)
        {
            // Delete the time entry from the database
            var result = _processor.DeleteTimeEntry(id);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetTimeEntries();
            }
            
            return Content(result.ErrorMessage);
        }

        /// <summary>
        /// Displays the report.
        /// </summary>
        public ActionResult WeeklyRevenueReport()
        {
            // Get the time entries for the month
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            var allEntries = _processor.GetTimesheetReportDataByWeek(firstDayOfMonth, lastDayOfMonth, User.Identity.Name);

            // Get the total for the month
            ViewData["RevenueCurrentMonthToDate"] = allEntries.Sum(e => e.TotalRevenue);

            return View();
        }

        /// <summary>
        /// Selects the report data.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        public ActionResult SelectWeeklyRevenueReportData(DateTime fromDate, DateTime toDate){
            
            // Get the data for the chart
            var entryReports = from e in _processor.GetTimesheetReportDataByWeek(fromDate, toDate, User.Identity.Name)
                               select new TimeEntryReport(e);

            return Json(entryReports);
        }

        #region Private Helpers
        /// <summary>
        /// Populates the projects lookup.
        /// </summary>
        private void PopulateProjectsLookup()
        {
            ViewBag.Projects = from p in _processor.GetProjects(User.Identity.Name)
                               where p.IsActive
                               orderby p.Client.ClientName, p.ProjectName
                               select new ProjectDropDownDisplay(p);
        }
        #endregion
    }
}
