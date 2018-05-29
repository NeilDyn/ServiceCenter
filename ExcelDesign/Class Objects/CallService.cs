using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.ServiceFunctions;

namespace ExcelDesign.Class_Objects
{
    public class CallService
    {
        private WebService webService;
        SearchResults currResults = new SearchResults();

        public CallService()
        {
            webService = new WebService();
        }

        public void callService(string searchNo)
        {
            currResults = webService.FindOrder(searchNo);
        }

        public List<SalesLine> returnLines()
        {
            List<SalesLine> sl = new List<SalesLine>();

            if((currResults.SalesShipmentLine != null) && (currResults.PostedPackageLine != null))
            {
                for (int i = 0; i < currResults.SalesShipmentLine.Length; i++)
                {
                    //sl.Add(new ShipmentLine(currResults.SalesShipmentLine[i].ItemNo,
                    //                        "Description",
                    //                        currResults.SalesShipmentLine[i].Qty,
                    //                        currResults.SalesShipmentLine[i].))
                }
            }

            return sl;
        }

        public SalesHeader GetSalesOrders()
        {
            SalesHeader returnSH = null;
            string status = null;
            string channelName = null;

            if (currResults.SOImportBuffer != null)
            {
                status = currResults.SOImportBuffer[0].OrderStatus;
                channelName = currResults.SOImportBuffer[0].ChannelName;
            }

            if((currResults.SalesHeader != null) && (currResults.SalesShipmentHeader != null) && (currResults.PostedPackage != null))
            {
               
                returnSH = new SalesHeader(status,
                                           currResults.SalesHeader[0].DocDate,
                                           currResults.SalesHeader[0].No,
                                           channelName,
                                           new ShipmentHeader(currResults.SalesShipmentHeader[0].No,
                                                              currResults.SalesShipmentHeader[0].ExtDocNo,
                                                              currResults.SalesShipmentHeader[0].ShippingDate,
                                                              currResults.SalesShipmentHeader[0].ShippingAgentService),
                                           new PostedPackage(currResults.PostedPackage[0].ExtTrackNo),
                                           currResults.SalesHeader[0].ExtDocNo);
            }

            return returnSH;
        }

        public Customer GetCustomerInfo()
        {
            Customer returnCust = null;

            if (currResults.SOImportBuffer != null)
            {
                returnCust = new Customer(currResults.SOImportBuffer[0].ShipToName,
                                        currResults.SOImportBuffer[0].ShipToAddress,
                                        currResults.SOImportBuffer[0].ShipToAddress2,
                                        currResults.SOImportBuffer[0].ShipToContact,
                                        currResults.SOImportBuffer[0].ShipToCity,
                                        currResults.SOImportBuffer[0].ShipToZip,
                                        currResults.SOImportBuffer[0].ShipToState,
                                        currResults.SOImportBuffer[0].ShipToCountry);
            }

            if (returnCust == null)
            {
                if (currResults.SalesHeader != null)
                {
                    returnCust = new Customer(currResults.SalesHeader[0].ShipToName,
                                        currResults.SalesHeader[0].ShipToAddress,
                                        currResults.SalesHeader[0].ShipToAddress2,
                                        currResults.SalesHeader[0].ShipToContact,
                                        currResults.SalesHeader[0].ShipToCity,
                                        currResults.SalesHeader[0].ShipToZip,
                                        currResults.SalesHeader[0].ShipToState,
                                        currResults.SalesHeader[0].ShipToCountry);
                }
            }

            if (returnCust == null)
            {
                if (currResults.SalesShipmentHeader != null)
                {
                    returnCust = new Customer(currResults.SalesShipmentHeader[0].ShipToName,
                                    currResults.SalesShipmentHeader[0].ShipToAddress,
                                    currResults.SalesShipmentHeader[0].ShipToAddress2,
                                    currResults.SalesShipmentHeader[0].ShipToContact,
                                    currResults.SalesShipmentHeader[0].ShipToCity,
                                    currResults.SalesShipmentHeader[0].ShipToZip,
                                    currResults.SalesShipmentHeader[0].ShipToState,
                                    currResults.SalesShipmentHeader[0].ShipToCountry);
                }
            }

            return returnCust;
        }
    }
}