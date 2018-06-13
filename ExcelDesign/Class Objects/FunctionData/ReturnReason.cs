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

        public ReturnReason()
        {

        }

        public ReturnReason(string reasonCodeP, string descriptionP)
        {
            this.ReasonCode = reasonCodeP;
            this.Description = descriptionP;
        }
    }
}