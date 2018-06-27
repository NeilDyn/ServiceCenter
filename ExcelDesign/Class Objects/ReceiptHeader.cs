using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReceiptHeader
    {
        public List<ReceiptLine> ReceiptLines { get; set; }
        public string ReceiptDate { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string No { get; set; }
        public string ShippingAgentCode { get; set; }
        public bool PopulatedFromShipmentHeader { get; set; }

        public ReceiptHeader()
        {

        }

        public ReceiptHeader(string noP, string externalDocumentNoP, string receiptDateP, List<ReceiptLine> receiptLinesP, 
            string shippingAgentCodeP, bool populatedFromShipmentHeaderP)
        {
            this.No = noP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.ReceiptDate = receiptDateP;
            this.ReceiptLines = receiptLinesP;
            this.ShippingAgentCode = shippingAgentCodeP;
            this.PopulatedFromShipmentHeader = populatedFromShipmentHeaderP;
        }
    }
}