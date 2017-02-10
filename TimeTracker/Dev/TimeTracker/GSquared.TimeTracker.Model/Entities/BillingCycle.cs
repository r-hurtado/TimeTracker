using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("BillingCycle")]
    public class BillingCycle
    {
        #region Primitive Properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillingCycleId { get; set; }

        public string BillingCycleDescription { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<Client> Clients { get; set; }

        #endregion
    }
}