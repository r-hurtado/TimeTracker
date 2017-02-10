namespace GSquared.TimeTracker.Model.Entities
{
    public class OpResult : IOpResult
    {
        #region IOpResult Members

        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// Gets or sets the error message corresponding to the <see cref="IOpResult"/>.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }

        #endregion
    }
}
