namespace GSquared.TimeTracker.Model.Entities
{
    /// <summary>
    /// Defines an interface for returning the results of a database operation
    /// </summary>
    public interface IOpResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is successful.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is successful; otherwise, <c>false</c>.
        /// </value>
        bool IsSuccessful  { get; set; }

        /// <summary>
        /// Gets or sets the error message corresponding to the <see cref="IOpResult"/>.
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage { get; set; }
    }
}
