using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.FunctionData
{
    public class ReturnReason
    {
        public string ReasonCode { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Display { get; set; }

        public ReturnReason()
        {

        }

        public ReturnReason(string reasonCodeP, string descriptionP, string categoryP)
        {
            this.ReasonCode = reasonCodeP;
            this.Description = descriptionP;
            this.Category = categoryP;
            this.Display = categoryP + "\t- " + descriptionP;
        }
    }
}