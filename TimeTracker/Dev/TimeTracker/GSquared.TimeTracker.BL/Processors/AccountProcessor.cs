using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class AccountProcessor : IAccountProcessor
    {
        private readonly ITimeTrackerRepository _db;

        public AccountProcessor()
        {
            _db = new TimeTrackerRepository();
        }

        public AccountProcessor(ITimeTrackerRepository repository)
        {
            _db = repository;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User.</returns>
        public User GetUser(string userName)
        {
            return _db.GetUser(userName);
        }

        public OpResult SaveProfile(UserProfile updatedProfile)
        {
            return _db.SaveProfile(updatedProfile);
        }
    }
}
