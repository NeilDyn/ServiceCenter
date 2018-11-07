using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PartialRefunded
    {
        public string OrderNo { get; set; }
        public string ExtDocNo { get; set; }
        public string ItemNo { get; set; }
        public string Description { get; set; }
        public string ReturnReason { get; set; }
        public double Price { get; set; }

        public PartialRefunded(string orderNoP, string extDocNoP, string itemNoP, string descriptionP, string returnReasonP, double priceP)
        {
            OrderNo = orderNoP;
            ExtDocNo = extDocNoP;
            ItemNo = itemNoP;
            Description = descriptionP;
            ReturnReason = returnReasonP;
            Price = priceP;
        }

        public PartialRefunded()
        {

        }
    }
}