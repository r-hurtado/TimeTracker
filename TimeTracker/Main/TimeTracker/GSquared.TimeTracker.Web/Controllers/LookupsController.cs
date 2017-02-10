using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using Telerik.Web.Mvc;

namespace GSquared.TimeTracker.Web.Controllers
{
    [Authorize]
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
        [GridAction]
        public ActionResult GetBillingTerms()
        {
            // Get the Billing terms from the database
            var terms = _processor.GetBillingTerms();

            return View(new GridModel<BillingTerm>(terms));
        }

        /// <summary>
        /// Adds the billing term.
        /// </summary>
        /// <param name="addedTerm">The added term.</param>
        [GridAction]
        public ActionResult AddBillingTerm(BillingTerm addedTerm)
        {
            // Add the term to the database
            var result = _processor.AddBillingTerm(addedTerm);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetBillingTerms();
            }
            
            return Content(result.ErrorMessage);
        }

        /// <summary>
        /// Updates the billing term.
        /// </summary>
        /// <param name="updatedTerm">The updated term.</param>
        [GridAction]
        public ActionResult UpdateBillingTerm(BillingTerm updatedTerm)
        {
            // Update the term
            var result = _processor.UpdateBillingTerm(updatedTerm);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetBillingTerms();
            }
            
            return Content(result.ErrorMessage);
        }

        /// <summary>
        /// Deletes the billing term.
        /// </summary>
        /// <param name="id">The id.</param>
        [GridAction]
        public ActionResult DeleteBillingTerm(int id)
        {
            // Delete the term
            var result = _processor.DeleteBillingTerm(id);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetBillingTerms();
            }
            
            return Content(result.ErrorMessage);
        }
    }
}
