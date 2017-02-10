using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GSquared.TimeTracker.Model.Entities
{
    [Table("BillingTerms")]
    public class BillingTerm
    {
        #region Primitive Properties

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BillingTermsId { get; set; }

        public string BillingTermsDescription { get; set; }

        public int NumberOfDaysToPay { get; set; }

        #endregion
    }
}