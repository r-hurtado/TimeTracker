using System;
using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Repository.Repositories
{
    public interface ITimeTrackerRepository
    {
        /// <summary>
        ///     Gets all time entries.
        /// </summary>
        /// <returns>
        ///     A list of <see cref="TimeEntry" /> objects
        /// </returns>
        IList<TimeEntry> GetAllTimeEntries();

        /// <summary>
        ///     Gets all time entries after.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        ///     A list of <see cref="TimeEntry" /> objects
        /// </returns>
        IList<TimesheetReportDataByWeek> GetTimesheetReportDataByWeek(DateTime startDate, DateTime endDate,
                                                                            string username);

        /// <summary>
        ///     Adds the entry.
        /// </summary>
        /// <param name="entryToAdd">The entry to add.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        IOpResult AddTimeEntry(TimeEntry entryToAdd, string userId);

        /// <summary>
        ///     Updates the entry.
        /// </summary>
        /// <param name="updatedEntry">The updated entry.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        IOpResult UpdateTimeEntry(TimeEntry updatedEntry);

        /// <summary>
        ///     Deletes the entry.
        /// </summary>
        /// <param name="timeEntryId">The time entry id.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        IOpResult DeleteTimeEntry(int timeEntryId);

        /// <summary>
        ///     Gets the clients.
        /// </summary>
        /// <returns>
        ///     a list of <see cref="Client" /> objects.
        /// </returns>
        IList<Client> GetClients();

        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="clientToAdd">The client to add.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult AddClient(Client clientToAdd);

        /// <summary>
        /// Updates the client.
        /// </summary>
        /// <param name="updatedClient">The updated client.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult UpdateClient(Client updatedClient);

        /// <summary>
        ///     Deletes the client.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>
        ///     An <see cref="IOpResult" /> object containing data including whether or
        ///     not the operation was successful and any error messages.
        /// </returns>
        IOpResult DeleteClient(int clientId);

        /// <summary>
        ///     Gets the projects.
        /// </summary>
        /// <returns>
        ///     a list of <see cref="Project" /> objects.
        /// </returns>
        IList<Project> GetProjects();

        /// <summary>
        /// Adds the Project.
        /// </summary>
        /// <param name="projectToAdd">The Project to add.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult AddProject(Project projectToAdd);

        /// <summary>
        /// Updates the Project.
        /// </summary>
        /// <param name="updatedProject">The updated Project.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult UpdateProject(Project updatedProject);

        /// <summary>
        /// Deletes the Project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        IOpResult DeleteProject(int projectId);

        /// <summary>
        ///     Gets the invoice header.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="quickbooksProject">The quickbooks project.</param>
        /// <returns>An Invoice Header for the given project and dates</returns>
        InvoiceHeader GetInvoiceHeader(DateTime fromDate, DateTime toDate, string quickbooksProject);

        /// <summary>
        ///     Gets the invoice entries.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="quickbooksProject">The quickbooks project.</param>
        /// <param name="includeProjectName">
        ///     if set to <c>true</c> include project name in the description.
        /// </param>
        /// <returns>
        ///     A list of <see cref="InvoiceEntry" /> objects for generating invoices
        /// </returns>
        IList<InvoiceEntry> GetInvoiceEntries(DateTime fromDate, DateTime toDate, string quickbooksProject,
                                              bool includeProjectName);

        /// <summary>
        ///     Gets the ITIM entries.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        ///     A list of <see cref="ItimeEntry" /> objects
        /// </returns>
        IList<ItimeEntry> GetItimeEntries(int clientId, int month, int year);

        /// <summary>
        ///     Gets the quickbooks time entries.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>A list of time entries between the two dates</returns>
        IList<TimeEntry> GetQuickbooksTimeEntries(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets the user with the specified User name.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User.</returns>
        User GetUser(string username);

        /// <summary>
        /// Gets the monthly time entries.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="fromDate">The From Date</param>
        /// <param name="toDate">The To Date</param>
        /// <returns>IList{TimeEntry}.</returns>
        IEnumerable<TimeEntry> GetMonthlyTimeEntries(string user, DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Saves the profile.
        /// </summary>
        /// <param name="updatedProfile">The updated profile.</param>
        /// <returns>OpResult.</returns>
        OpResult SaveProfile(UserProfile updatedProfile);
    }
}