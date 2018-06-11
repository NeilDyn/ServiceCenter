using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ShipmentHeader
    {
        public List<ShipmentLine> ShipmentLines { get; set; }
        public string ShippingAgentCode { get; set; }
        public string ShippingAgentService { get; set; }
        public string ShippingDate { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string No { get; set; }

        public ShipmentHeader()
        {

        }

        public ShipmentHeader(string noP, string externalDocumentNoP, string shippingDateP, string shippingAgentServiceP, string shippingAgentCodeP,
            List<ShipmentLine> shipmentLinesP)
        {
            this.No = noP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.ShippingDate = shippingDateP;
            this.ShippingAgentService = shippingAgentServiceP;
            this.ShippingAgentCode = shippingAgentCodeP;
            this.ShipmentLines = shipmentLinesP;
        }
    }
}