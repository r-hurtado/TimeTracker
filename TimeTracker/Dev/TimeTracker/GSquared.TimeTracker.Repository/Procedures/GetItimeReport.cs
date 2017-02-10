using GSquared.TimeTracker.Model.Entities;
using GSquared.TimeTracker.Repository.Extensions;

namespace GSquared.TimeTracker.Repository.Procedures
{
    public class GetItimeReport : DatabaseExtensions.IStoredProcedure<ItimeEntry>
    {
        public string ProcedureName
        {
            get { return "usp_GetITimReport"; }
        }

        public int ClientId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
