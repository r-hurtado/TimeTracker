using System;
using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;

namespace GSquared.TimeTracker.Repository.Procedures
{
    public class GetInvoiceHeader : DatabaseExtensions.IStoredProcedure<InvoiceHeader>
    {
        public string ProcedureName
        {
            get { return "usp_GetInvoiceHeader"; }
        }

        public string QuickbooksProjectId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
