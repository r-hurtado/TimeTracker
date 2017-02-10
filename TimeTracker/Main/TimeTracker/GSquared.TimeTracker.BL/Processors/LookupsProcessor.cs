using System.Collections.Generic;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class LookupsProcessor : ILookupsProcessor
    {
        private readonly ILookupsRepository _db;

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="LookupsProcessor"/> class.
        /// </summary>
        public LookupsProcessor()
        {
            _db = new LookupsRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupsProcessor"/> class.
        /// </summary>
        /// <param name="db">The db.</param>
        public LookupsProcessor(ILookupsRepository db)
        {
            _db = db;
        } 
        #endregion

        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        /// <returns>IList{BillingTerm}.</returns>
        public IList<BillingTerm> GetBillingTerms()
        {
            return _db.GetBillingTerms();
        }

        /// <summary>
        /// Adds the billing term.
        /// </summary>
        /// <param name="addedTerm">The added term.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult AddBillingTerm(BillingTerm addedTerm)
        {
            return _db.AddBillingTerm(addedTerm);
        }

        /// <summary>
        /// Updates the billing term.
        /// </summary>
        /// <param name="updatedTerm">The updated term.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult UpdateBillingTerm(BillingTerm updatedTerm)
        {
            return _db.UpdateBillingTerm(updatedTerm);
        }

        /// <summary>
        /// Deletes the billing term.
        /// </summary>
        /// <param name="billingTermId">The billing term id.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult DeleteBillingTerm(int billingTermId)
        {
            return _db.DeleteBillingTerm(billingTermId);
        }
    }
}
