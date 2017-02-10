using System.ComponentModel.DataAnnotations;
using GSquared.TimeTracker.Model.Entities;

namespace GSquared.TimeTracker.Web.Models
{
    public class ClientModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientModel"/> class.
        /// </summary>
        /// <param name="entity">The entity containing data for initializing the model.</param>
        public ClientModel(Client entity)
        {
            ClientId = entity.ClientId;
            ClientName = entity.ClientName;
            BillingTermsId = entity.BillingTermsId;
            BillingTermsDescription = entity.BillingTerm.BillingTermsDescription;
            BillingCycleId = entity.BillingCycleId;
            BillingCycleDescription = entity.BillingCycle.BillingCycleDescription;
            IsActive = entity.IsActive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientModel"/> class.
        /// </summary>
        public ClientModel() { }

        /// <summary>
        /// Gets or sets the client id corresponding to the <see cref="ClientModel"/>.
        /// </summary>
        /// <value>The client id.</value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        /// </summary>
        /// <value>The name of the client.</value>
        [Required]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the billing terms id corresponding to the <see cref="ClientModel"/>.
        /// </summary>
        /// <value>The billing terms id.</value>
        [UIHint("BillingTerms")]
        [Required]
        public int? BillingTermsId { get; set; }

        /// <summary>
        /// Gets or sets the billing terms description corresponding to the <see cref="ClientModel"/>.
        /// </summary>
        /// <value>The billing terms description.</value>
        public string BillingTermsDescription { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle id.
        /// </summary>
        /// <value>
        /// The billing cycle id.
        /// </value>
        [UIHint("BillingCycles")]
        [Required]
        public int? BillingCycleId { get; set; }

        /// <summary>
        /// Gets or sets the billing cycle description.
        /// </summary>
        /// <value>
        /// The billing cycle description.
        /// </value>
        public string BillingCycleDescription { get; set; }

        /// <summary>
        /// Determines whether or not the Client is Active
        /// </summary>
        public bool IsActive { get; set; }
    }
}