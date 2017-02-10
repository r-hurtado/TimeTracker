using System;

namespace GSquared.TimeTracker.Model.Entities
{
    public class TimeEntry
    {
        #region Primitive Properties

        public int TimeEntryId { get; set; }
        public Guid UserId { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime? ToTime { get; set; }
        public decimal TotalTime { get; set; }
        public DateTime DateWorked { get; set; }
        public bool IsBillable { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public int ProjectId { get; set; }

        #endregion

        #region Navigation Properties

        public User User { get; set; }
        public Project Project { get; set; }

        #endregion
    }
}
