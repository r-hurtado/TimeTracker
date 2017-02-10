using System;
using System.Collections.Generic;
using System.Linq;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Properties;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{

    public class ClientProcessor : IClientProcessor
    {

        private readonly ITimeTrackerRepository _db;
        private readonly ILookupsRepository _lookups;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProcessor" /> class.
        /// </summary>
        public ClientProcessor()
        {
            _db = new TimeTrackerRepository();
            _lookups = new LookupsRepository();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientProcessor" /> class.
        /// </summary>
        /// <param name="db">The db.</param>
        /// <param name="lookups">The lookups.</param>
        public ClientProcessor(ITimeTrackerRepository db, ILookupsRepository lookups)
        {
            _db = db;
            _lookups = lookups;
        }

        #endregion

        /// <summary>
        /// Gets the clients for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        public IEnumerable<Client> GetClients(string username)
        {
            return _db.GetClients().Where(c => c.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="newClient">The new client.</param>
        /// <param name="username">The username.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult AddClient(Client newClient, string username)
        {
            var userEntity = _db.GetUser(username);
            if (userEntity == null)
            {
                throw new ArgumentException(Resources.InvalidUserIdMessage);
            }
            newClient.UserId = userEntity.UserId;
            return _db.AddClient(newClient);
        }

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="updatedClient">The updated client.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult UpdateClient(Client updatedClient)
        {
            return _db.UpdateClient(updatedClient);
        }

        /// <summary>
        /// Deletes the client.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>IOpResult.</returns>
        public IOpResult DeleteClient(int clientId)
        {
            return _db.DeleteClient(clientId);
        }

        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        /// <returns>IEnumerable{BillingTerm}.</returns>
        public IEnumerable<BillingTerm> GetBillingTerms()
        {
            return _lookups.GetBillingTerms();
        }

        /// <summary>
        /// Gets the billing cycles.
        /// </summary>
        /// <returns>IEnumerable{BillingCycle}.</returns>
        public IEnumerable<BillingCycle> GetBillingCycles()
        {
            return _lookups.GetBillingCycles();
        }
    }
}