using System;
using System.Collections.Generic;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.BL.Interfaces
{
    public interface IInvoicingProcessor
    {
        /// <summary>
        /// Gets the time entries formatted for exporting to QuickBooks as an Invoice.
        /// </summary>
        /// <param name="quickbooksProject">The quickbooks project.</param>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="username">The username.</param>
        /// <param name="includeProjectName">if set to <c>true</c> include the project name in invoice entries.</param>
        /// <param name="nextInvoice">The next invoice.</param>
        /// <returns>System.String.</returns>
        string GetQuickbooksInvoice(string quickbooksProject, DateTime fromDate, DateTime toDate, string username, bool includeProjectName, int nextInvoice);

        /// <summary>
        /// Gets the time entries formatted for export to QuickBooks.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <param name="username">The username.</param>
        /// <returns>System.String.</returns>
        string GetQuickbooksTimesheet(DateTime fromDate, DateTime toDate, string username);

        /// <summary>
        /// Gets the time sheet data formatted for ODOT ITIM.
        /// </summary>
        /// <param name="clientId">The client id.</param>
        /// <param name="month">The month.</param>
        /// <param name="year">The year.</param>
        /// <returns>System.String.</returns>
        string GetItimeReport(int clientId, int month, int year);

        /// <summary>
        /// Gets the active clients for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        IEnumerable<Client> GetActiveClients(string username);

        /// <summary>
        /// Gets the quickbooks projects for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{System.String}.</returns>
        IEnumerable<string> GetQuickbooksProjects(string username);
    }
}
