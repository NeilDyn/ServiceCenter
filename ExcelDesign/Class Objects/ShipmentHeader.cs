using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ShipmentHeader
    {
        private string no;
        private string externalDocumentNo;
        private string shippingDate;
        private string shippingAgentService;
        private string shippingAgentCode;
        private List<ShipmentLine> shipmentLines;

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

        public List<ShipmentLine> ShipmentLines
        {
            get { return shipmentLines; }
            set { shipmentLines = value; }
        }

        public string ShippingAgentCode
        {
            get { return shippingAgentCode; }
            set { shippingAgentCode = value; }
        }

        public string ShippingAgentService
        {
            get { return shippingAgentService; }
            set { shippingAgentService = value; }
        }
        

        public string ShippingDate
        {
            get { return shippingDate; }
            set { shippingDate = value; }
        }
        

        public string ExternalDocumentNo
        {
            get { return externalDocumentNo; }
            set { externalDocumentNo = value; }
        }
        

        public string No
        {
            get { return no; }
            set { no = value; }
        }
        
    }
}