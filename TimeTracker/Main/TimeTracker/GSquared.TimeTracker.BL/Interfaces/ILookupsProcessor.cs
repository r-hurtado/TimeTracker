using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface ILookupsProcessor
    {
        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        /// <returns>IList{BillingTerm}.</returns>
        IList<BillingTerm> GetBillingTerms();

        /// <summary>
        /// Adds the billing term.
        /// </summary>
        /// <param name="addedTerm">The added term.</param>
        /// <returns>IOpResult.</returns>
        IOpResult AddBillingTerm(BillingTerm addedTerm);

        /// <summary>
        /// Updates the billing term.
        /// </summary>
        /// <param name="updatedTerm">The updated term.</param>
        /// <returns>IOpResult.</returns>
        IOpResult UpdateBillingTerm(BillingTerm updatedTerm);

        /// <summary>
        /// Deletes the billing term.
        /// </summary>
        /// <param name="billingTermId">The billing term id.</param>
        /// <returns>IOpResult.</returns>
        IOpResult DeleteBillingTerm(int billingTermId);
    }
}
