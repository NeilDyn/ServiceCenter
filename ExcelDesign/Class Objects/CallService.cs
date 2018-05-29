using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.ServiceFunctions;
using System.Linq;

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

        private List<ShipmentLine> ReturnShipmentLines(string no)
        {
            List<ShipmentLine> shipLine = null;

            string itemNo = null;
            string description = null;
            int quantity = 0;
            int quantityShipped = 0;
            double price = 0;
            double lineAmount = 0;

            for (int sl = 0; sl < currResults.SalesShipmentLine.Length; sl++)
            {
                if(currResults.SalesShipmentLine[sl].DocNo == no)
                {
                    itemNo      = currResults.SalesShipmentLine[sl].ItemNo;
                    description = currResults.SalesShipmentLine[sl].Description;
                    int.TryParse(currResults.SalesShipmentLine[sl].Qty, out quantity);
                    double.TryParse(currResults.SalesShipmentLine[sl].UnitPrice, out price);
                    lineAmount  = quantity * price;

                    for (int sli = 0; sli < currResults.SalesShipmentLine.Length; sli++)
                    {
                        if((currResults.SalesShipmentLine[sli].DocNo == no) && (currResults.SalesShipmentLine[sli].ItemNo == itemNo))
                        {
                            int currQty = 0;
                            int.TryParse(currResults.SalesShipmentLine[sli].Qty, out currQty);
                            quantityShipped += currQty;
                        }
                    }

                    shipLine.Add(new ShipmentLine(no, description, quantity, quantityShipped, price, lineAmount));

                    itemNo = null;
                    description = null;
                    quantity = 0;
                    quantityShipped = 0;
                    price = 0;
                    lineAmount = 0;
                }
            }

            return shipLine;
        }

        private List<PostedPackage> ReturnPostedPackage()
        {
            List<PostedPackage> postPackage = null;

            return postPackage;
        }

        private List<ShipmentHeader> ReturnShipmentHeader(string orderNo)
        {
            List<ShipmentHeader> shipHeader = null;

            string no = null;
            string externalDocumentNo = null;
            string shippingDate = null;
            string shippingAgentService = null;
            List<ShipmentLine> shipLine = null;

            for (int sh = 0; sh < currResults.SalesShipmentHeader.Length; sh++)
            {
                if (currResults.SalesShipmentHeader[sh].OrderNo == orderNo)
                {
                    no                      = currResults.SalesShipmentHeader[sh].No;
                    externalDocumentNo      = currResults.SalesShipmentHeader[sh].ExtDocNo;
                    shippingDate            = currResults.SalesShipmentHeader[sh].ShippingDate;
                    shippingAgentService    = currResults.SalesShipmentHeader[sh].ShippingAgentService;
                    shipLine = ReturnShipmentLines(no);

                    shipHeader.Add(new ShipmentHeader(no, externalDocumentNo, shippingDate, shippingAgentService, shipLine));

                    no = null;
                    externalDocumentNo = null;
                    shippingDate = null;
                    shippingAgentService = null;
                    shipLine = null;
                }
            }

            return shipHeader;
        }

        public List<SalesHeader> GetSalesOrders()
        {
            List<SalesHeader> returnSH = null;            
            
            string orderStatus = null;
            string orderDate = null;
            string orderNo = null;
            string channelName = null;
            List<ShipmentHeader> shipHeader = null;
            List<PostedPackage> postPackage = null;           
            string externalDocumentNo = null;

            List<string> insertedOrderNumbers = null;

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    if (!insertedOrderNumbers.Contains(orderNo))
                    {
                        orderStatus = currResults.SOImportBuffer[so].OrderStatus;
                        orderDate = currResults.SOImportBuffer[so].OrderDate;
                        channelName = currResults.SOImportBuffer[so].ChannelName[0];
                        orderNo = currResults.SOImportBuffer[so].SalesOrderNo;
                        externalDocumentNo = currResults.SOImportBuffer[so].ExternalDocumentNo;

                        shipHeader = ReturnShipmentHeader(orderNo);

                        insertedOrderNumbers.Add(orderNo);
                    }
                }
            }

            if((currResults.SalesHeader != null) && (currResults.SalesShipmentHeader != null) && (currResults.PostedPackage != null))
            {
                if (!insertedOrderNumbers.Contains(orderNo))
                {

                }
                returnSH.Add(new SalesHeader(orderStatus,
                                             orderDate,
                                             currResults.SalesHeader[0].No,
                                             channelName,
                                             shipHead,
                                             postPack,
                                             currResults.SalesHeader[0].ExtDocNo));

                //new ShipmentHeader(currResults.SalesShipmentHeader[0].No,
                //                   currResults.SalesShipmentHeader[0].ExtDocNo,
                //                   currResults.SalesShipmentHeader[0].ShippingDate,
                //                   currResults.SalesShipmentHeader[0].ShippingAgentService),
                //new PostedPackage(currResults.PostedPackage[0].ExtTrackNo),
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