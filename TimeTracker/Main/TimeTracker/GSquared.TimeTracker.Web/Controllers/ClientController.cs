using System.Linq;
using System.Web.Mvc;
using GSquared.TimeTracker.BL.Interfaces;
using GSquared.TimeTracker.BL.Processors;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Web.Models;
using Telerik.Web.Mvc;

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
            PopulateBillingTermsLookup();
            PopulateBillingCycleLookup();
            return View();
        }

        /// <summary>
        /// Gets the clients from the database.
        /// </summary>
        [GridAction]
        public ActionResult GetClients()
        {
            // Get the list of clients from the database
            var clients = from c in _processor.GetClients(User.Identity.Name)
                          select new ClientModel(c);

            return View(new GridModel<ClientModel>(clients));
        }

        [GridAction]
        public ActionResult AddClient(Client clientToAdd)
        {
            // Add the client to the database
            var result = _processor.AddClient(clientToAdd, User.Identity.Name);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetClients();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult UpdateClient(Client updatedClient)
        {
            // Update the client in the database
            var result = _processor.UpdateClient(updatedClient);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetClients();
            }
            
            return Content(result.ErrorMessage);
        }

        [GridAction]
        public ActionResult DeleteClient(int deletedClientId)
        {
            // Delete the client from the database
            var result = _processor.DeleteClient(deletedClientId);

            // Return either the updated list or an error message
            if (result.IsSuccessful)
            {
                return GetClients();
            }
            
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
