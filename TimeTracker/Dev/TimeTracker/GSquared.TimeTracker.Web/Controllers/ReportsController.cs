using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Table;

namespace GSquared.TimeTracker.Web.Controllers
{
    public class ReportsController : Controller
    {
        private readonly ReportsProcessor _processor;

        public ReportsController()
        {
            _processor = new ReportsProcessor();
        }

        public ActionResult IncomeByWeek()
        {
            return View();
        }

        public ActionResult GetWeeklyIncomeData()
        {
            var fromDate = DateTime.Now.AddMonths(-1);
            var toDate = DateTime.Now;

            // Get the time entries for the month
            // Get the data for the chart
            var entryReports = (from e in _processor.GetTimesheetReportDataByWeek(fromDate, toDate, User.Identity.Name)
                               select new TimeEntryReport(e)).ToList();

            // Use the data to build an array of two-dimensional arrays
            var income =
                new
                    {
                        Name = "income",
                        data =
                            entryReports.Select(
                                e =>
                                new object[] {e.Week, e.TotalRevenue})
                                      .ToArray()
                    };

            var hours =
                new
                    {
                        Name = "time",
                        data =
                            entryReports.Select(
                                e => new object[] {e.Week, e.TotalHours})
                                      .ToArray()
                    };

            var incomeAndHours = new object[] {income, hours};

            return Json(incomeAndHours, JsonRequestBehavior.AllowGet);
        }

        public ActionResult MonthlyTimesheet()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetMonthlyTimesheet(DateTime fromDate, DateTime toDate)
        {
            // Get timesheet data for the current user
            var timeEntries = _processor.GetMonthlyTimeEntries(User.Identity.Name, fromDate, toDate);

            // Get the User information
            var userInformation = _processor.GetUser(User.Identity.Name);

            var fullName = userInformation.Profile == null ? User.Identity.Name : userInformation.Profile.FullName;

            // Throw the data into excel and Return the spreadsheet to the browser
            using (var monthlyTimesheet = GetMonthlyTimesheetSpreadsheet(timeEntries, fullName, fromDate, toDate))
            {
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", string.Format("attachment;  filename=MonthlyTimesheet_{0}_{1}.xlsx", fromDate.ToString("yyyyMMdd"), toDate.ToString("yyyyMMdd")));
                Response.BinaryWrite(monthlyTimesheet.GetAsByteArray());
            }

            return Content(string.Empty);
        }

        private ExcelPackage GetMonthlyTimesheetSpreadsheet(IEnumerable<TimeEntry> timesheetData, string fullName, DateTime fromDate, DateTime toDate)
        {
            var package = new ExcelPackage();

            var entryList = timesheetData.ToList();

            // Summarize the data
            var summarizedData =
                entryList.GroupBy(t => t.DateWorked)
                    .Select(t => new {DateWorked = t.Key, TotalTime = t.Sum(s => s.TotalTime)})
                    .OrderBy(t => t.DateWorked)
                    .ToList();

            // Get the totals by Client
            var totalsByClient =
                entryList.GroupBy(t => t.Project.Client.ClientName)
                    .Select(t => new {ClientName = t.Key, TotalHours = t.Sum(s => s.TotalTime)})
                    .OrderBy(t => t.ClientName)
                    .ToList();
            
            //Create the worksheet
            var worksheet = package.Workbook.Worksheets.Add("MonthlyTimesheet");
            worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
            worksheet.PrinterSettings.FitToPage = true;

            // Set Column Widths
            worksheet.Column(1).Width = 20;
            worksheet.Column(2).Width = 30;
            worksheet.Column(3).Width = 5;
            worksheet.Column(4).Width = 20;
            worksheet.Column(5).Width = 35;
            worksheet.Row(5).Height = 24;
            worksheet.Row(6).Height = 24;

            // Populate the Header
            worksheet.Cells["A1"].Value = "G-Squared Software Timesheet";
            worksheet.Cells["A3"].Value = "Employee Name:";
            worksheet.Cells["B3"].Value = fullName;
            worksheet.Cells["D3"].Value = "Date Range:";
            worksheet.Cells["E3"].Value = fromDate.ToString("MM/dd/yyyy") + " to " + toDate.ToString("MM/dd/yyyy");
            worksheet.Cells["A4"].Value = "Signatures";            
            worksheet.Cells["A5"].Value = "Employee:";
            worksheet.Cells["D5"].Value = "Supervisor:";
            worksheet.Cells["A6"].Value = "I hereby certify that the hours shown herein are a complete and accurate record of the hours worked by me for the reporting period.";

            // Format the header
            worksheet.Cells["A1:A5,D3:D5"].Style.Font.Bold = true;
            worksheet.Cells["A4"].Style.Font.UnderLine = true;
            worksheet.Cells["B5"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet.Cells["E5"].Style.Border.BorderAround(ExcelBorderStyle.Thin);
            worksheet.Cells["A6"].Style.Font.Size = 8;

            // Load the collection into the sheet, starting from cell A7. Print the column names on row 1
            worksheet.Cells["A7"].LoadFromCollection(summarizedData, true, TableStyles.Light1);

            // Format the data
            using (var col = worksheet.Cells[8, 2, 8 + summarizedData.Count, 2])
            {
                col.Style.Numberformat.Format = "#,##0.00";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            using (var col = worksheet.Cells[8, 1, 8 + summarizedData.Count, 1])
            {
                col.Style.Numberformat.Format = "MM/dd/yyyy";
                col.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
            }

            // Set up the total
            using (var monthlyTotalCell = worksheet.Cells["B" + (8 + summarizedData.Count)])
            {
                monthlyTotalCell.Formula = "Sum(B8:B" + (7 + summarizedData.Count) + ")";
                monthlyTotalCell.Style.Font.Bold = true;
            }

            using (var monthlyTotalCell = worksheet.Cells["A" + (8 + summarizedData.Count).ToString(CultureInfo.CurrentCulture) + ":B" + (8 + summarizedData.Count).ToString(CultureInfo.CurrentCulture)])
            {
                monthlyTotalCell.Style.Font.Bold = true;
                monthlyTotalCell.Style.Border.BorderAround(ExcelBorderStyle.Double);
            }
            worksheet.Cells["A" + (8 + summarizedData.Count)].Value = "Total Hours:";

            // Print the Summary By Client
            worksheet.Cells["A" + (10 + summarizedData.Count)].LoadFromCollection(totalsByClient, true,
                TableStyles.Light1);

            return package;
            
        }
    }
}
