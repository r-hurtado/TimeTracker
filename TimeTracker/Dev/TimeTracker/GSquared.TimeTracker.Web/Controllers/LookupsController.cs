using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;

namespace GSquared.TimeTracker.Web.Controllers
{
    
    public class LookupsController : Controller
    {
        private readonly ILookupsProcessor _processor;

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupsController"/> class.
        /// </summary>
        public LookupsController()
        {
            _processor = new LookupsProcessor();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LookupsController" /> class.
        /// </summary>
        /// <param name="processor">The processor.</param>
        public LookupsController(ILookupsProcessor processor)
        {
            _processor = processor;
        }

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets the billing terms.
        /// </summary>
        public ActionResult GetBillingTerms(JqGridParametersModel parameters)
        {
            // Get the Billing terms from the database
            var terms = _processor.GetBillingTerms()
                                  .OrderBy(bt => bt.BillingTermsDescription)
                                  .Skip(parameters.Rows*(parameters.Page - 1))
                                  .Take(parameters.Rows).ToList();
            var recordCount = terms.Count;

            return Json(
                new JqJsonModel<BillingTerm>(terms)
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

        public ActionResult SaveBillingTerm(BillingTerm term, string oper, string id)
        {
            IOpResult result;

            switch (oper)
            {
                case "edit":
                    result = _processor.UpdateBillingTerm(term);
                    break;
                case "add":
                    result = _processor.AddBillingTerm(term);
                    break;
                case "del":
                    result = _processor.DeleteBillingTerm(int.Parse(id));
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
    }
}
