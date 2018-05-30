using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ExcelDesign.ServiceFunctions;
using System.Linq;
using System.Globalization;

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
            List<ShipmentLine> shipLine = new List<ShipmentLine>();

            string itemNo = null;
            string description = null;
            int quantity = 0;
            int quantityShipped = 0;
            double price = 0;
            double lineAmount = 0;

            if (currResults.SalesShipmentLine != null)
            {
                for (int sl = 0; sl < currResults.SalesShipmentLine.Length; sl++)
                {
                    if (currResults.SalesShipmentLine[sl].DocNo == no)
                    {
                        itemNo = currResults.SalesShipmentLine[sl].ItemNo;
                        description = currResults.SalesShipmentLine[sl].Description;
                        int.TryParse(currResults.SalesShipmentLine[sl].Qty, out quantity);
                        double.TryParse(currResults.SalesShipmentLine[sl].UnitPrice, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        for (int sli = 0; sli < currResults.SalesShipmentLine.Length; sli++)
                        {
                            if ((currResults.SalesShipmentLine[sli].DocNo == no) && (currResults.SalesShipmentLine[sli].ItemNo == itemNo))
                            {
                                int currQty = 0;
                                int.TryParse(currResults.SalesShipmentLine[sli].Qty, out currQty);
                                quantityShipped += currQty;
                            }
                        }

                        shipLine.Add(new ShipmentLine(itemNo, description, quantity, quantityShipped, price, lineAmount));

                        itemNo = null;
                        description = null;
                        quantity = 0;
                        quantityShipped = 0;
                        price = 0;
                        lineAmount = 0;
                    }
                }
            }

            return shipLine;
        }

        private List<PostedPackageLine> ReturnPostedPackageLine(string postedPackageNo)
        {
            List<PostedPackageLine> postedPackageLine = new List<PostedPackageLine>();

            string serialNo = null;
            string packageNo = null;
            string itemNo = null;
            string description = null;
            int quantity = 0;
            string type = null;
            double price = 0;

            if (currResults.PostedPackageLine != null)
            {
                for (int ppl = 0; ppl < currResults.PostedPackageLine.Length; ppl++)
                {
                    if (currResults.PostedPackageLine[ppl].PackNo == postedPackageNo)
                    {
                        serialNo = currResults.PostedPackageLine[ppl].SerialNo;
                        packageNo = currResults.PostedPackageLine[ppl].PackNo;
                        itemNo = currResults.PostedPackageLine[ppl].ItemNo;
                        description = currResults.PostedPackageLine[ppl].Description;
                        type = currResults.PostedPackageLine[ppl].Type;
                        int.TryParse(currResults.PostedPackageLine[ppl].Qty, out quantity);
                        double.TryParse(currResults.PostedPackageLine[ppl].Price, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);

                        postedPackageLine.Add(new PostedPackageLine(serialNo, packageNo, itemNo, description, quantity, price));

                        serialNo = null;
                        packageNo = null;
                        itemNo = null;
                        description = null;
                        quantity = 0;
                        type = null;
                        price = 0;
                    }
                }
            }

            return postedPackageLine;
        }

        private List<PostedPackage> ReturnPostedPackage(string orderNo)
        {
            List<PostedPackage> postPackage = new List<PostedPackage>();

            string trackingNo = null;
            string packageNo = null;
            string shippingAgent = null;
            string shippingAgentService = null;
            string packDate = null;
            string sourceID = null;
            string postedSourceID = null;
            List<PostedPackageLine> postedPackageLines = new List<PostedPackageLine>();

            if (currResults.PostedPackage != null)
            {
                for (int pp = 0; pp < currResults.PostedPackage.Length; pp++)
                {
                    if (currResults.PostedPackage[pp].SourceID == orderNo)
                    {
                        trackingNo = currResults.PostedPackage[pp].ExtTrackNo;
                        packageNo = currResults.PostedPackage[pp].PackNo;
                        shippingAgent = currResults.PostedPackage[pp].ShippingAgent;
                        shippingAgentService = currResults.PostedPackage[pp].ShippingService;
                        packDate = currResults.PostedPackage[pp].PostingDate;
                        sourceID = currResults.PostedPackage[pp].SourceID;
                        postedSourceID = currResults.PostedPackage[pp].PostedSourceID;
                        postedPackageLines = ReturnPostedPackageLine(packageNo);

                        postPackage.Add(new PostedPackage(trackingNo, packageNo, shippingAgent, shippingAgentService, packDate, sourceID, postedSourceID, postedPackageLines));

                        trackingNo = null;
                        packageNo = null;
                        shippingAgent = null;
                        shippingAgentService = null;
                        packDate = null;
                        sourceID = null;
                        postedSourceID = null;
                        postedPackageLines = new List<PostedPackageLine>();
                    }
                }
            }

            return postPackage;
        }

        private List<ShipmentHeader> ReturnShipmentHeader(string orderNo)
        {
            List<ShipmentHeader> shipHeader = new List<ShipmentHeader>();

            string no = null;
            string externalDocumentNo = null;
            string shippingDate = null;
            string shippingAgentService = null;
            List<ShipmentLine> shipLine = new List<ShipmentLine>();

            if (currResults.SalesShipmentHeader != null)
            {
                for (int sh = 0; sh < currResults.SalesShipmentHeader.Length; sh++)
                {
                    if (currResults.SalesShipmentHeader[sh].OrderNo == orderNo)
                    {
                        no = currResults.SalesShipmentHeader[sh].No;
                        externalDocumentNo = currResults.SalesShipmentHeader[sh].ExtDocNo;
                        shippingDate = currResults.SalesShipmentHeader[sh].ShippingDate;
                        shippingAgentService = currResults.SalesShipmentHeader[sh].ShippingAgentService;
                        shipLine = ReturnShipmentLines(no);

                        shipHeader.Add(new ShipmentHeader(no, externalDocumentNo, shippingDate, shippingAgentService, shipLine));

                        no = null;
                        externalDocumentNo = null;
                        shippingDate = null;
                        shippingAgentService = null;
                        shipLine = new List<ShipmentLine>();
                    }
                }
            }
            return shipHeader;
        }

        public List<SalesHeader> GetSalesOrders()
        {
            List<SalesHeader> salesHead = new List<SalesHeader>();
            string type = null;

            string orderStatus = null;
            string orderDate = null;
            string orderNo = null;
            string channelName = null;
            List<ShipmentHeader> shipHeader = new List<ShipmentHeader>();
            List<PostedPackage> postPackage = new List<PostedPackage>();
            string externalDocumentNo = null;
            Warranty warranty = null;

            string status = null;
            string policy = null;
            string daysRemaining = null;

            List<string> insertedOrderNumbers = new List<string>();

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    orderNo = currResults.SOImportBuffer[so].SalesOrderNo;

                    if (!insertedOrderNumbers.Any(order=> order.Equals(orderNo)))
                    {
                        //type = currResults.SOImportBuffer[so].
                        orderStatus = currResults.SOImportBuffer[so].OrderStatus;
                        orderDate = currResults.SOImportBuffer[so].OrderDate;
                        channelName = currResults.SOImportBuffer[so].ChannelName[0];                      
                        externalDocumentNo = currResults.SOImportBuffer[so].ExternalDocumentNo;
                        shipHeader = ReturnShipmentHeader(orderNo);
                        postPackage = ReturnPostedPackage(orderNo);

                        status = currResults.SOImportBuffer[so].Warranty[0].Status[0];
                        policy = currResults.SOImportBuffer[so].Warranty[0].Policy[0]; ;
                        daysRemaining = currResults.SOImportBuffer[so].Warranty[0].DaysRemaining[0];
                        warranty = new Warranty(status, policy, daysRemaining);

                        salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty));

                        insertedOrderNumbers.Add(orderNo);

                        orderStatus = null;
                        orderDate = null;
                        orderNo = null;
                        channelName = null;
                        shipHeader = new List<ShipmentHeader>();
                        postPackage = new List<PostedPackage>();
                        externalDocumentNo = null;
                        warranty = null;

                        status = null;
                        policy = null;
                        daysRemaining = null;
                    }
                }
            }
            else if (currResults.SalesHeader != null)
            {
                for (int so = 0; so < currResults.SalesHeader.Length; so++)
                {

                    orderStatus = "Unknown"; // To be Discussed
                    orderDate = currResults.SalesHeader[so].DocDate;
                    channelName = currResults.SalesHeader[so].SellToCustomerName;
                    orderNo = currResults.SalesHeader[so].No;
                    externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;
                    shipHeader = ReturnShipmentHeader(orderNo);
                    postPackage = ReturnPostedPackage(orderNo);

                    status = currResults.SalesHeader[so].Warranty2[0].Status2[0];
                    policy = currResults.SalesHeader[so].Warranty2[0].Policy2[0]; ;
                    daysRemaining = currResults.SalesHeader[so].Warranty2[0].DaysRemaining2[0];
                    warranty = new Warranty(status, policy, daysRemaining);

                    salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty));

                    insertedOrderNumbers.Add(orderNo);

                    orderStatus = null;
                    orderDate = null;
                    orderNo = null;
                    channelName = null;
                    shipHeader = null;
                    postPackage = null;
                    externalDocumentNo = null;
                    warranty = null;

                    status = null;
                    policy = null;
                    daysRemaining = null;
                }
            }

            return salesHead;
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