using System.Collections.Generic;

namespace GSquared.TimeTracker.Web.Models
{
    /// <summary>
    /// This is the base class for building data grids
    /// </summary>
    public class JqJsonModel<T>
    {
        public JqJsonModel(IEnumerable<T> dataItems)
        {
            Rows = dataItems;
        }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>The current page.</value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the total pages.
        /// </summary>
        /// <value>The total pages.</value>
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the record count.
        /// </summary>
        /// <value>The record count.</value>
        public int RecordCount { get; set; }

        /// <summary>
        /// Gets or sets the data items.
        /// </summary>
        /// <value>The data items.</value>
        public IEnumerable<T> Rows { get; set; }
    }
}