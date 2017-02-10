using System;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;

namespace GSquared.TimeTracker.Repository.Procedures
{
    public class GetInvoiceEntries : DatabaseExtensions.IStoredProcedure<InvoiceEntry>
    {
        public string QuickbooksProjectId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IncludeProjectNameInDescription { get; set; }

        public string ProcedureName
        {
            get { return "usp_GetInvoiceEntries"; }
        }
    }
}