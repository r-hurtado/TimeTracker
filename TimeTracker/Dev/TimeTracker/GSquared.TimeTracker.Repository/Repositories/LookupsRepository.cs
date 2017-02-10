using System;
using System.Collections.Generic;
using System.Linq;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Repository.Repositories
{
    public class LookupsRepository : ILookupsRepository
    {
        /// <summary>
        ///     Gets the billing terms.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="BillingTerm" /> objects
        /// </returns>
        public IList<BillingTerm> GetBillingTerms()
        {
            List<BillingTerm> billingTerms;

            using (var ctx = new TimeTrackerContext())
            {
                // Get an ordered list of Billing Terms
                var terms = from t in ctx.BillingTerms
                            orderby t.BillingTermsDescription
                            select t;

                billingTerms = terms.ToList();
            }

            return billingTerms;
        }

        /// <summary>
        ///     Gets the billing cycles.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="BillingCycle" /> objects
        /// </returns>
        public IList<BillingCycle> GetBillingCycles()
        {
            using (var ctx = new TimeTrackerContext())
            {
                return ctx.BillingCycles.OrderBy(bc => bc.BillingCycleDescription).ToList();
            }
        }

        /// <summary>
        ///     Adds the billing term to the database.
        /// </summary>
        /// <param name="termToAdd">The term to add.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        public IOpResult AddBillingTerm(BillingTerm termToAdd)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Add the object
                    ctx.BillingTerms.Add(termToAdd);
                    ctx.SaveChanges();

                    // Return success
                    result.IsSuccessful = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Return failure
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        ///     Updates the billing term.
        /// </summary>
        /// <param name="updatedTerm">The updated term.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        public IOpResult UpdateBillingTerm(BillingTerm updatedTerm)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Update the object
                    var existingEntity = ctx.BillingTerms.First(b => b.BillingTermsId == updatedTerm.BillingTermsId);
                    ctx.Entry(existingEntity).CurrentValues.SetValues(updatedTerm);
                    ctx.SaveChanges();

                    // Return success
                    result.IsSuccessful = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Return failure
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Deletes the billing term.
        /// </summary>
        /// <param name="deletedTermId">The deleted term id.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult DeleteBillingTerm(int deletedTermId)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Delete the object
                    var term = ctx.BillingTerms.First(t => t.BillingTermsId == deletedTermId);
                    ctx.BillingTerms.Remove(term);
                    ctx.SaveChanges();

                    // Return success
                    result.IsSuccessful = true;
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Return failure
                result.IsSuccessful = false;
                result.ErrorMessage = ex.Message;
                return result;
            }
        }

        /// <summary>
        /// Gets the quickbooks projects.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Returns a list of Quickbooks projects</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IList<string> GetQuickbooksProjects(string username)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var projects = from p in ctx.Projects
                               where
                                   !string.IsNullOrEmpty(p.QuickbooksProjectId) && p.IsActive &&
                                   p.Client.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase)
                               select p.QuickbooksProjectId;

                return projects.Distinct().OrderBy(p => p).ToList();
            }
        }
    }
}