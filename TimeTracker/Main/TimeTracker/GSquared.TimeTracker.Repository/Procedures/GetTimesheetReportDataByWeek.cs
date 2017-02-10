using System;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;

namespace GSquared.TimeTracker.Repository.Procedures
{
    public class GetTimesheetReportDataByWeek : DatabaseExtensions.IStoredProcedure<TimesheetReportDataByWeek>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Username { get; set; }

        public string ProcedureName
        {
            get { return "usp_GetTimesheetReportDataByWeek"; }
        }
    }
}
