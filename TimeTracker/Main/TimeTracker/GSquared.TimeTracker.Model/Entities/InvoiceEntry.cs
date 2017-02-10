namespace GSquared.TimeTracker.Model.Entities
{
    public class InvoiceEntry
    {
        #region Primitive Properties

        public string DateWorked { get; set; }

        public string Description { get; set; }

        public decimal? Hours { get; set; }

        public decimal Rate { get; set; }

        public decimal? Amount { get; set; }

        #endregion
    }
}