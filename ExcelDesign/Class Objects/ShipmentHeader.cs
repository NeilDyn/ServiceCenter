using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ShipmentHeader
    {
        public List<ShipmentLine> ShipmentLines { get; set; }
        public List<ReceiptLine> ReturnLines { get; set; }
        public string ShippingAgentCode { get; set; }
        public string ShippingAgentService { get; set; }
        public string ShippingDate { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string No { get; set; }
        public string SellToCustomerNo { get; set; }
        public List<string> RMANo { get; set; }
        public bool GeneratedFromSalesHeader { get; set; }

        public ShipmentHeader()
        {

        }

        public ShipmentHeader(string noP, string externalDocumentNoP, string shippingDateP, string shippingAgentServiceP, string shippingAgentCodeP,
            List<ShipmentLine> shipmentLinesP, string sellToCustomerNoP, List<ReceiptLine> returnLinesP, List<string> rmaNoP, bool generatedFromSalesHeaderP)
        {
            this.No = noP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.ShippingDate = shippingDateP;
            this.ShippingAgentService = shippingAgentServiceP;
            this.ShippingAgentCode = shippingAgentCodeP;
            this.ShipmentLines = shipmentLinesP;
            this.SellToCustomerNo = sellToCustomerNoP;
            this.ReturnLines = returnLinesP;
            this.RMANo = rmaNoP;
            this.GeneratedFromSalesHeader = generatedFromSalesHeaderP;
        }
    }
}