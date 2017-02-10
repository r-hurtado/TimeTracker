using System;
using System.Collections.Generic;
using System.Linq;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;
using GSquared.TimeTracker.Repository.Procedures;

namespace GSquared.TimeTracker.Repository.Repositories
{
    public class TimeTrackerRepository : ITimeTrackerRepository
    {
        /// <summary>
        /// Gets all time entries.
        /// </summary>
        /// <returns>A list of <see cref="TimeEntry"/> objects</returns>
        public IList<TimeEntry> GetAllTimeEntries()
        {
            List<TimeEntry> timeEntryList;

            using (var ctx = new TimeTrackerContext())
            {
                // Get an ordered list of Billing Terms
                var entries = from e in ctx.TimeEntries.Include("Project").Include("Project.Client")
                              orderby e.DateWorked descending
                              select e;

                timeEntryList = entries.ToList();
            }

            return timeEntryList;
        }


        /// <summary>
        /// Gets all time entries between the given dates.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="username">The username.</param>
        /// <returns>A list of <see cref="TimesheetReportDataByWeek" /> objects</returns>
        public IList<TimesheetReportDataByWeek> GetTimesheetReportDataByWeek(DateTime startDate, DateTime endDate, string username)
        {
            // Instantiate the result list
            List<TimesheetReportDataByWeek> timeEntryList;

            using (var ctx = new TimeTrackerContext())
            {
                var proc = new GetTimesheetReportDataByWeek
                               {
                                   FromDate = startDate,
                                   ToDate = endDate,
                                   Username = username
                               };

                // Get an ordered list of Billing Terms
                var data = ctx.Database.ExecuteStoredProcedure(proc);

                // Convert the data to an List
                timeEntryList = data.ToList();
            }

            return timeEntryList;
        }

        /// <summary>
        /// Adds the entry.
        /// </summary>
        /// <param name="entryToAdd">The entry to add.</param>
        /// <param name="userId">The user id.</param>
        /// <returns>
        /// An <see cref="IOpResult"/> object containing data including whether or 
        /// not the operation was successful and any error messages.
        /// </returns>
        public IOpResult AddTimeEntry(TimeEntry entryToAdd, string userId)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Capture the create date/time and user
                    entryToAdd.CreatedAt = DateTime.Now;
                    entryToAdd.CreatedBy = userId;

                    // Add the object
                    ctx.TimeEntries.Add(entryToAdd);
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
        /// Updates the entry.
        /// </summary>
        /// <param name="updatedEntry">The updated entry.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult UpdateTimeEntry(TimeEntry updatedEntry)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Update the object
                    var entry = ctx.TimeEntries.Single(t => t.TimeEntryId == updatedEntry.TimeEntryId);
                    updatedEntry.CreatedAt = entry.CreatedAt;
                    updatedEntry.CreatedBy = entry.CreatedBy;
                    updatedEntry.UserId = entry.UserId;
                    ctx.Entry(entry).CurrentValues.SetValues(updatedEntry);
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
        /// Deletes the entry.
        /// </summary>
        /// <param name="timeEntryId">The time entry id.</param>
        /// <returns>
        /// An <see cref="IOpResult"/> object containing data including whether or 
        /// not the operation was successful and any error messages.
        /// </returns>
        public IOpResult DeleteTimeEntry(int timeEntryId)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Delete the object
                    var entry = ctx.TimeEntries.First(e => e.TimeEntryId == timeEntryId);
                    ctx.TimeEntries.Remove(entry);
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
        /// Gets the clients.
        /// </summary>
        /// <returns>a list of <see cref="Client"/> objects.</returns>
        public IList<Client> GetClients()
        {
            List<Client> clientList;

            using (var ctx = new TimeTrackerContext())
            {
                // Get an ordered list of Billing Terms
                var clients = from c in ctx.Clients.Include("BillingTerm").Include("BillingCycle").Include("User")
                            orderby c.ClientName
                            select c;

                clientList = clients.ToList();
            }

            return clientList;
        }

        /// <summary>
        /// Adds the client.
        /// </summary>
        /// <param name="clientToAdd">The client to add.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult AddClient(Client clientToAdd)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Add the object
                    ctx.Clients.Add(clientToAdd);
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
        /// Updates the client.
        /// </summary>
        /// <param name="updatedClient">The updated client.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult UpdateClient(Client updatedClient)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Update the object
                    var client = ctx.Clients.First(c => c.ClientId == updatedClient.ClientId);
                    updatedClient.UserId = client.UserId;
                    ctx.Entry(client).CurrentValues.SetValues(updatedClient);
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
        /// Deletes the client.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <returns>
        /// An <see cref="IOpResult"/> object containing data including whether or 
        /// not the operation was successful and any error messages.
        /// </returns>
        public IOpResult DeleteClient(int clientId)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Delete the object
                    var client = ctx.Clients.First(c => c.ClientId == clientId);
                    ctx.Clients.Remove(client);
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
        /// Gets the projects.
        /// </summary>
        /// <returns>a list of <see cref="Project"/> objects.</returns>
        public IList<Project> GetProjects()
        {
            List<Project> projectList;

            using (var ctx = new TimeTrackerContext())
            {
                // Get an ordered list of Billing Terms
                var projects = from p in ctx.Projects.Include("Client").Include("Client.User")
                              orderby p.ProjectName
                              select p;

                projectList = projects.ToList();
            }

            return projectList;
        }

        /// <summary>
        /// Adds the Project.
        /// </summary>
        /// <param name="projectToAdd">The Project to add.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult AddProject(Project projectToAdd)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Add the object
                    ctx.Projects.Add(projectToAdd);
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
        /// Updates the Project.
        /// </summary>
        /// <param name="updatedProject">The updated Project.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult UpdateProject(Project updatedProject)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Update the object
                    var project = ctx.Projects.First(p => p.ProjectId == updatedProject.ProjectId);
                    ctx.Entry(project).CurrentValues.SetValues(updatedProject);
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
        /// Deletes the Project.
        /// </summary>
        /// <param name="projectId">The project id.</param>
        /// <returns>An <see cref="IOpResult" /> object containing data including whether or
        /// not the operation was successful and any error messages.</returns>
        public IOpResult DeleteProject(int projectId)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Delete the object
                    var project = ctx.Projects.First(c => c.ProjectId == projectId);
                    ctx.Projects.Remove(project);
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
        /// Gets the invoice header.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="quickbooksProject">The quickbooks project.</param>
        /// <returns>
        /// An Invoice Header for the given project and dates
        /// </returns>
        public InvoiceHeader GetInvoiceHeader(DateTime fromDate, DateTime toDate, string quickbooksProject)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var proc = new GetInvoiceHeader
                               {
                                   FromDate = fromDate,
                                   ToDate = toDate,
                                   QuickbooksProjectId = quickbooksProject
                               };
                var header = ctx.Database.ExecuteStoredProcedure(proc);
                return header.FirstOrDefault();
            }
        }

        /// <summary>
        /// Gets the invoice entries.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="quickbooksProject">The quickbooks project.</param>
        /// <param name="includeProjectName">if set to <c>true</c> include project name in the description.</param>
        /// <returns>
        /// A list of <see cref="InvoiceEntry" /> objects for generating invoices
        /// </returns>
        public IList<InvoiceEntry> GetInvoiceEntries(DateTime fromDate, DateTime toDate, string quickbooksProject, bool includeProjectName)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var proc = new GetInvoiceEntries
                               {
                                   FromDate = fromDate,
                                   ToDate = toDate,
                                   QuickbooksProjectId = quickbooksProject,
                                   IncludeProjectNameInDescription = includeProjectName
                               };
                var entries = ctx.Database.ExecuteStoredProcedure(proc);
                return entries.ToList();
            }
        }

        /// <summary>
        /// Gets the ITIM entries.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>
        /// A list of <see cref="ItimeEntry" /> objects
        /// </returns>
        public IList<ItimeEntry> GetItimeEntries(int clientId, int month, int year)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var proc = new GetItimeReport
                               {
                                   ClientId = clientId,
                                   Month = month,
                                   Year = year
                               };
                var entries = ctx.Database.ExecuteStoredProcedure(proc);
                return entries.ToList();
            }
        }

        /// <summary>
        /// Gets the quickbooks time entries.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns>
        /// A list of time entries between the two dates
        /// </returns>
        public IList<TimeEntry> GetQuickbooksTimeEntries(DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var entries = from e in ctx.TimeEntries.Include("Project")
                              where e.DateWorked >= fromDate
                                    && e.DateWorked <= toDate
                                    && !string.IsNullOrEmpty(e.Project.QuickbooksProjectId)
                              select e;
                return entries.ToList();
            }
        }

        /// <summary>
        /// Gets the user with the specified User name.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>User.</returns>
        public User GetUser(string username)
        {
            using (var ctx = new TimeTrackerContext())
            {
                return
                    ctx.Users.Include("Profile").FirstOrDefault(u => u.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
            }
        }

        /// <summary>
        /// Gets the monthly time entries.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="fromDate">The From Date</param>
        /// <param name="toDate">The To Date</param>
        /// <returns>IList{TimeEntry}.</returns>
        public IEnumerable<TimeEntry> GetMonthlyTimeEntries(string user, DateTime fromDate, DateTime toDate)
        {
            using (var ctx = new TimeTrackerContext())
            {
                var updatedToDate = toDate.AddHours(23).AddMinutes(59).AddSeconds(59);
                return
                    ctx.TimeEntries.Include("Project").Include("Project.Client")
                        .Where(
                            t =>
                                t.CreatedBy.Equals(user, StringComparison.CurrentCultureIgnoreCase) &&
                                t.DateWorked >= fromDate && t.DateWorked <= updatedToDate).ToList();
            }
        }

        /// <summary>
        /// Saves the profile.
        /// </summary>
        /// <param name="updatedProfile">The updated profile.</param>
        /// <returns>OpResult.</returns>
        public OpResult SaveProfile(UserProfile updatedProfile)
        {
            var result = new OpResult();

            try
            {
                using (var ctx = new TimeTrackerContext())
                {
                    // Get the User profile
                    var profile = ctx.UserProfiles.FirstOrDefault(p => p.UserId.Equals(updatedProfile.UserId));

                    // If there isn't a profile to edit
                    if (profile == null)
                    {
                        // Add the profile
                        ctx.UserProfiles.Add(updatedProfile);
                        ctx.SaveChanges();
                    }
                    else
                    {
                        // Update the object
                        ctx.Entry(profile).CurrentValues.SetValues(updatedProfile);
                        ctx.SaveChanges();
                    }

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
    }
}
