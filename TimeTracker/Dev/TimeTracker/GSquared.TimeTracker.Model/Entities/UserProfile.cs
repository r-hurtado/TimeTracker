using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public bool ExportHoursToQuickbooks { get; set; }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        /// <value>The full name.</value>
        public string FullName
        {
            get
            {
                return FirstName +
                       string.Format("{0} ", string.IsNullOrWhiteSpace(MiddleInitial) ? string.Empty : " " + MiddleInitial) +
                       LastName;
            }
        }

        public User UserProfileUser { get; set; }
    }
}
