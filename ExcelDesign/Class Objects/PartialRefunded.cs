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
        public double RefundAmount { get; set; }
        public double RefundSalesTax { get; set; }
        public double RefundShippingTax { get; set; }

        public PartialRefunded(string orderNoP, string extDocNoP, string itemNoP, string descriptionP, string returnReasonP, double refundAmount,
                               double refundSalesTax, double refundShippingTax)
        {
            OrderNo = orderNoP;
            ExtDocNo = extDocNoP;
            ItemNo = itemNoP;
            Description = descriptionP;
            ReturnReason = returnReasonP;
            RefundAmount = refundAmount;
        }

        public PartialRefunded()
        {

        }
    }
}