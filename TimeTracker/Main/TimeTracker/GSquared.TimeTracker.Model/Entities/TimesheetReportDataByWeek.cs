using System;

namespace GSquared.TimeTracker.Model.Entities
{
    public class TimesheetReportDataByWeek
    {
        #region Primitive Properties

        public DateTime? Week { get; set; }

        public decimal? TotalTime { get; set; }

        public decimal? TotalRevenue { get; set; }

        public decimal RevenueGoal { get; set; }

        public decimal TimeGoal { get; set; }

        #endregion
    }
}