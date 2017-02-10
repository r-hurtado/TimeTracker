using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Repositories;

namespace GSquared.TimeTracker.BL.Processors
{
    public class InvoicingProcessor : IInvoicingProcessor
    {
        private readonly ITimeTrackerRepository _db;
        private readonly ILookupsRepository _lookups;

        #region Constructors
        public InvoicingProcessor()
        {
            _db = new TimeTrackerRepository();
            _lookups = new LookupsRepository();
        }

        public InvoicingProcessor(ITimeTrackerRepository db, ILookupsRepository lookups)
        {
            _db = db;
            _lookups = lookups;
        } 
        #endregion

        public string GetQuickbooksInvoice(string quickbooksProject, DateTime fromDate, DateTime toDate, string username, bool useProjectName, int nextInvoice)
        {
            // Get the data
            var fileHeader = _db.GetInvoiceHeader(fromDate, toDate, quickbooksProject);
            var invoiceHeaders = _db.GetInvoiceEntries(fromDate, toDate, quickbooksProject, useProjectName);

            // Build the file
            var sb = new StringBuilder();
            sb.AppendLine("!TRNS	TRNSID	TRNSTYPE	DATE	ACCNT	NAME	CLASS	AMOUNT	DOCNUM	MEMO	CLEAR	TOPRINT	NAMEISTAXABLE	ADDR1	ADDR3	TERMS	SHIPVIA	SHIPDATE");
            sb.AppendLine("!SPL	SPLID	TRNSTYPE	DATE	ACCNT	NAME	CLASS	AMOUNT	DOCNUM	MEMO	CLEAR	QNTY	PRICE	INVITEM	TAXABLE	OTHER3	YEARTODATE	WAGEBASE");
            sb.AppendLine("!ENDTRNS																	");
            sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\r\n",
                "TRNS", "", "INVOICE", toDate.ToString("MM/dd/yyyy"), "Accounts Receivable", quickbooksProject, "", Math.Round(fileHeader.Total.HasValue ? fileHeader.Total.Value : 0, 2), nextInvoice, "", "N", "N", "N", "", "", fileHeader.BillingTermsDescription, "", toDate.ToString("MM/dd/yyyy"));

            // Add the invoice headers
            foreach (var entry in invoiceHeaders)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}\t{12}\t{13}\t{14}\t{15}\t{16}\t{17}\r\n",
                    "SPL", "", "INVOICE", entry.DateWorked, "Sales - Support and Maintenance", "", "", entry.Amount * -1, "", entry.Description, "N", entry.Hours * -1, Math.Round(entry.Rate, 2), "Programming - Standard", "N", "Darren Boss", "0", "0");
            }

            sb.AppendLine("ENDTRNS																	");

            return sb.ToString();
        }

        public string GetQuickbooksTimesheet(DateTime fromDate, DateTime toDate, string username)
        {
            // Get the data
            IList<TimeEntry> timeEntries = _db.GetQuickbooksTimeEntries(fromDate, toDate);

            // Build the file
            var sb = new StringBuilder();
            sb.AppendLine("!TIMEACT	DATE	JOB	EMP	ITEM	DURATION	NOTE	BILLINGSTATUS");
            foreach (var entry in timeEntries)
            {
                sb.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\r\n",
                    "TIMEACT", entry.DateWorked.ToString("MM/dd/yyyy"), entry.Project.QuickbooksProjectId, "Darren Boss", "Programming - Standard", entry.TotalTime.ToString(), entry.Description, entry.IsBillable ? "1" : "0");
            }

            return sb.ToString();
        }

        public string GetItimeReport(int clientId, int month, int year)
        {
            var itimData = _db.GetItimeEntries(clientId, month, year);
            var currentDate = new DateTime(1900, 1, 1);

            var sb = new StringBuilder("ITIM Data:\r\n");
            foreach (var itimEntry in itimData)
            {
                if (currentDate != itimEntry.DateWorked)
                {
                    sb.AppendLine(itimEntry.DateWorked.ToString("MM/dd/yyyy"));
                    currentDate = itimEntry.DateWorked;
                }
                sb.AppendLine(itimEntry.ITIMLine);
            }

            return sb.ToString();
        }

        /// <summary>
        /// Gets the active clients for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{Client}.</returns>
        public IEnumerable<Client> GetActiveClients(string username)
        {
            return
                _db.GetClients()
                   .Where(c => c.IsActive && c.User.UserName.Equals(username, StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Gets the quickbooks projects for the given user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>IEnumerable{System.String}.</returns>
        public IEnumerable<string> GetQuickbooksProjects(string username)
        {
            return _lookups.GetQuickbooksProjects(username);
        }
    }
}
