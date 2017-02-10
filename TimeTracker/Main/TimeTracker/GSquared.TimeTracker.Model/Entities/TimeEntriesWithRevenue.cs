using System;

namespace GSquared.TimeTracker.Model.Entities
{
    public class TimeEntriesWithRevenue
    {
        #region Primitive Properties

        public int TimeEntryId { get; set; }

        public Guid UserId { get; set; }

        public decimal TotalTime { get; set; }

        public DateTime DateWorked { get; set; }

        public bool IsBillable { get; set; }

        public decimal HourlyBillingRate { get; set; }

        public int? Week { get; set; }

        public decimal? TotalRevenue { get; set; }

        #endregion
    }
}