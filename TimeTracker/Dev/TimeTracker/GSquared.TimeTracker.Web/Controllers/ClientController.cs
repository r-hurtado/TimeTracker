using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController"/> class.
        /// </summary>
        public ClientController()
        {
            _processor = new ClientProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientController" /> class.
        /// </summary>
        /// <param name="processor">The entity processor.</param>
        public ClientController(IClientProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the clients from the database.
        /// </summary>
        public ActionResult GetClients(JqGridParametersModel parameters)
        {
            // Get the list of clients from the database
            var clients = _processor.GetClients(User.Identity.Name)
                                    .OrderBy(c => c.ClientName)
                                    .Select(c => new ClientModel(c))
                                    .Skip(parameters.Rows*(parameters.Page - 1))
                                    .Take(parameters.Rows).ToList();
            var recordCount = clients.Count;

            return Json(
                new JqJsonModel<ClientModel>(clients)
                    {
                        CurrentPage = parameters.Page == 0 ? 1 : parameters.Page,
                        RecordCount = recordCount,
                        TotalPages =
                            recordCount%parameters.Rows == 0
                                ? recordCount/parameters.Rows
                                : (recordCount/parameters.Rows) + 1
                    },
                JsonRequestBehavior.AllowGet
                );
        }

        public ActionResult GetBillingTermsDropDown()
        {
            PopulateBillingTermsLookup();

            return PartialView("EditorTemplates/BillingTerms");
        }

        public ActionResult GetBillingCyclesDropDown()
        {
            PopulateBillingCycleLookup();

            return PartialView("EditorTemplates/BillingCycles");
        }

        public ActionResult SaveClient(ClientModel model, string oper, string id)
        {
            IOpResult result;

            switch (oper)
            {
                case "edit":
                    result = _processor.UpdateClient(model.ToEntity());
                    break;
                case "add":
                    result = _processor.AddClient(model.ToEntity(), User.Identity.Name);
                    break;
                case "del":
                    result = _processor.DeleteClient(int.Parse(id));
                    break;
                default:
                    result = new OpResult
                    {
                        IsSuccessful = false,
                        ErrorMessage = string.Format("{0} is an unsupported operation.", oper)
                    };
                    break;
            }

            // Return an empty string
            if (result.IsSuccessful)
            {
                return Content(string.Empty);
            }

            // Return the error message
            HttpContext.Response.StatusCode = 500;
            return Content(result.ErrorMessage);
        }

        #region Private Helpers
        /// <summary>
        /// Populates the billing terms lookup.
        /// </summary>
        private void PopulateBillingTermsLookup()
        {
            ViewBag.BillingTerms = _processor.GetBillingTerms();
        }

        /// <summary>
        /// Populates the billing cycle lookup.
        /// </summary>
        private void PopulateBillingCycleLookup()
        {
            ViewBag.BillingCycles = _processor.GetBillingCycles();
        } 
        #endregion
    }
}
