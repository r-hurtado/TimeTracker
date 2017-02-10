using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("aspnet_Users")]
    public class User
    {
        #region Primitive Properties

        public Guid ApplicationId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string LoweredUserName { get; set; }

        public string MobileAlias { get; set; }

        public bool IsAnonymous { get; set; }

        public DateTime LastActivityDate { get; set; }

        #endregion

        #region Navigation Properties

        public virtual ICollection<TimeEntry> TimeEntries { get; set; }

        #endregion
    }
}