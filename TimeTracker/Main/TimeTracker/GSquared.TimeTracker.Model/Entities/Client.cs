using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("Clients")]
    public class Client
    {
        #region Primitive Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Required]
        public string ClientName { get; set; }

        [Required]
        public int BillingTermsId { get; set; }

        [Required]
        public int BillingCycleId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        public bool IsActive { get; set; }

        #endregion

        #region Navigation Properties

        public BillingTerm BillingTerm { get; set; }

        public ICollection<Project> Projects { get; set; }

        public BillingCycle BillingCycle { get; set; }

        public User User { get; set; }

        #endregion
    }
}