using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface IClientProcessor
    {
        /// <summary>
        /// Gets the clients for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        IEnumerable<Client> GetClients(string username);

        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="newClient">The new client.</param>
        /// <param name="username">The username.</param>
        /// <returns>IOpResult.</returns>
        IOpResult AddClient(Client newClient, string username);

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="updatedClient">The updated client.</param>
        /// <returns>IOpResult.</returns>
        IOpResult UpdateClient(Client updatedClient);

        /// <summary>
        /// Deletes the client.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>IOpResult.</returns>
        IOpResult DeleteClient(int clientId);

        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        /// <returns>IEnumerable{BillingTerm}.</returns>
        IEnumerable<BillingTerm> GetBillingTerms();

        /// <summary>
        /// Gets the billing cycles.
        /// </summary>
        /// <returns>IEnumerable{BillingCycle}.</returns>
        IEnumerable<BillingCycle> GetBillingCycles();
    }
}
