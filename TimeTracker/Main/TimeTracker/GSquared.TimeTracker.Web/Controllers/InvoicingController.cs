using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;

namespace GSquared.TimeTracker.Web.Controllers
{
    public class InvoicingController : Controller
    {
        private readonly IInvoicingProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController"/> class.
        /// </summary>
        public InvoicingController()
        {
            _processor = new InvoicingProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectController" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public InvoicingController(IInvoicingProcessor processor)
        {
            _processor = processor;
        }

        //
        // GET: /Invoicing/

        public ActionResult Index()
        {
            PopulateQuickbooksProjects();
            PopulateClientsLookup();
            return View();
        }

        public ActionResult GetQuickbooksDataExport(string quickbooksProject, DateTime fromDate, DateTime toDate, bool useProjectName, int nextInvoice)
        {
            // Get the file contents
            var fileContents = _processor.GetQuickbooksInvoice(quickbooksProject, fromDate, toDate, User.Identity.Name,
                                                               useProjectName, nextInvoice);
            // Return the formatted file
            return File(Encoding.UTF8.GetBytes(fileContents), "text/plain", string.Format("{0}_{1}_{2}.iif", quickbooksProject, toDate.ToString("MMM"), toDate.Year));
        }

        public ActionResult GetQuickbooksTimesheetExport(DateTime fromDate, DateTime toDate)
        {
            // Get the file contents
            var fileContents = _processor.GetQuickbooksTimesheet(fromDate, toDate, User.Identity.Name);

            // Return the formatted file
            return File(Encoding.UTF8.GetBytes(fileContents), "text/plain", string.Format("{0}_{1}_{2}.iif", "Timesheet", toDate.ToString("MMM"), toDate.Year));
        }

        public ActionResult GetItimData(int clientId, int month, int year)
        {
            return Content(_processor.GetItimeReport(clientId, month, year));
        }

        /// <summary>
        /// Populates the quickbooks projects.
        /// </summary>
        private void PopulateQuickbooksProjects()
        {
            var projects = _processor.GetQuickbooksProjects(User.Identity.Name);
            var list = new SelectList(projects);
            ViewBag.QuickbooksProjects = list;
        }

        /// <summary>
        /// Populates the clients lookup.
        /// </summary>
        private void PopulateClientsLookup()
        {
            // Get a list of Active clients
            ViewBag.Clients = _processor.GetActiveClients(User.Identity.Name).ToList();
        }

    }
}
