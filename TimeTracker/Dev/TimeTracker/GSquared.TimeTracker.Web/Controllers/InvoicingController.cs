using System;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
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
            
            return View();
        }

        public ActionResult ItimeReport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetClientList(string contains)
        {
            return
                Json(
                    _processor.GetActiveClients(User.Identity.Name)
                              .Where(c => c.ClientName.ToUpper().Contains(contains.ToUpper()))
                              .Select(c => new {label = c.ClientName, value = c.ClientId})
                              .ToList(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult QuickbooksTimesheetExport()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
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


    }
}
