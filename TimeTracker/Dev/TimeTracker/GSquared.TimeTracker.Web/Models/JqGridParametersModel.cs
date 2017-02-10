using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GSquared.TimeTracker.Web.Models
{
    public class JqGridParametersModel
    {
        public bool Search { get; set; }
        public int Page { get; set; }
        public int Rows { get; set; }
        public string Sidx { get; set; }
        public string Sortd { get; set; }
    }
}