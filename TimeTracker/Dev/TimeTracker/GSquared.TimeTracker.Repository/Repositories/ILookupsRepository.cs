using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Repository.Repositories
{
    
    public interface ILookupsRepository
    {
        #region Get Lookup Lists
        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        /// <returns>A list of <see cref="BillingTerm" /> objects</returns>
        IList<BillingTerm> GetBillingTerms();

        /// <summary>
        /// Gets the billing cycles.
        /// </summary>
        /// <returns>A list of <see cref="BillingCycle"/> objects</returns>
        IList<BillingCycle> GetBillingCycles();

        /// <summary>
        /// Gets the quickbooks projects.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Returns a list of Quickbooks projects</returns>
        IList<string> GetQuickbooksProjects(string username);
        #endregion

        #region Manage Lookup Lists

        /// <summary>
        /// Adds the billing term to the database.
        /// </summary>
        /// <param name="termToAdd">The term to add.</param>
        /// <returns>
        /// An <see cref="IOpResult"/> object containing data including whether or 
        /// not the operation was successful and any error messages.
        /// </returns>
        IOpResult AddBillingTerm(BillingTerm termToAdd);

        /// <summary>
        /// Updates the billing term.
        /// </summary>
        /// <param name="updatedTerm">The updated term.</param>
        /// <returns>
        /// An <see cref="IOpResult"/> object containing data including whether or
        /// not the operation was successful and any error messages.
        /// </returns>
        IOpResult UpdateBillingTerm(BillingTerm updatedTerm);

        /// <summary>
        /// Deletes the billing term.
        /// </summary>
        /// <param name="deletedTermId">The deleted term id.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult DeleteBillingTerm(int deletedTermId);

        #endregion

        
    }
}
