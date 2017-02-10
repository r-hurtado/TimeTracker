using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface ITimeEntryProcessor
    {
        /// <summary>
        /// Gets all time entries.
        /// </summary>
        /// <returns>IEnumerable{TimeEntry}.</returns>
        IEnumerable<TimeEntry> GetAllTimeEntries(string username);

        /// <summary>
        /// Gets all of the projects the given user can access.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Project}.</returns>
        IEnumerable<Project> GetProjects(string username);

        /// <summary>
        /// Adds the time entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <param name="username">The username.</param>
        /// <returns>IOpResult.</returns>
        IOpResult AddTimeEntry(TimeEntry entry, string username);

        /// <summary>
        /// Updates the time entry.
        /// </summary>
        /// <param name="entry">The entry.</param>
        /// <returns>IOpResult.</returns>
        IOpResult UpdateTimeEntry(TimeEntry entry);

        /// <summary>
        /// Deletes the time entry.
        /// </summary>
        /// <param name="entryId">The entry id.</param>
        /// <returns>IOpResult.</returns>
        IOpResult DeleteTimeEntry(int entryId);
    }
}
