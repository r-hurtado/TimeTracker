using System;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;

namespace GSquared.TimeTracker.Repository.Procedures
{
    public class GetTimesheetReportDataByWeek : DatabaseExtensions.IStoredProcedure<TimesheetReportDataByWeek>
    {
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Username { get; set; }

        public string ProcedureName
        {
            get { return "usp_GetTimesheetReportDataByWeek"; }
        }
    }
}
