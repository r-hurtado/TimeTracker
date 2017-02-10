using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("Projects")]
    public class Project
    {
        #region Primitive Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProjectId { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public string BillingCode { get; set; }

        public string QuickbooksProjectId { get; set; }

        public decimal HourlyBillingRate { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Navigation Properties

        public Client Client { get; set; }

        public ICollection<TimeEntry> TimeEntries { get; set; }

        #endregion
    }
}