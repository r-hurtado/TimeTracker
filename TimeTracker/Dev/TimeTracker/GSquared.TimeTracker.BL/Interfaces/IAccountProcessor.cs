using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface IAccountProcessor
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns>User.</returns>
        User GetUser(string userName);

        /// <summary>
        /// Saves the profile.
        /// </summary>
        /// <param name="updatedProfile">The updated profile.</param>
        /// <returns>OpResult.</returns>
        OpResult SaveProfile(UserProfile updatedProfile);
    }
}
