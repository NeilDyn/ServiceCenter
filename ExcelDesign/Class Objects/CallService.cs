using System.Collections.Generic;
using System.Linq;
using ExcelDesign.ServiceFunctions;
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

        public void OpenService(string searchNo)
        {
            currResults = webService.FindOrder(searchNo);
        }

        private List<PostedPackageLine> ReturnPostedPackageLine(string postedPackageNo)
        {
            List<PostedPackageLine> postedPackageLine = new List<PostedPackageLine>();

            string serialNo = string.Empty;
            string packageNo = string.Empty;
            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            string type = string.Empty;
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

                        serialNo = string.Empty;
                        packageNo = string.Empty;
                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        type = string.Empty;
                        price = 0;
                    }
                }
            }

            return postedPackageLine;
        }

        private List<PostedPackage> ReturnPostedPackage(string orderNo)
        {
            List<PostedPackage> postPackage = new List<PostedPackage>();

            string trackingNo = string.Empty;
            string packageNo = string.Empty;
            string shippingAgent = string.Empty;
            string shippingAgentService = string.Empty;
            string packDate = string.Empty;
            string sourceID = string.Empty;
            string postedSourceID = string.Empty;
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

                        trackingNo = string.Empty;
                        packageNo = string.Empty;
                        shippingAgent = string.Empty;
                        shippingAgentService = string.Empty;
                        packDate = string.Empty;
                        sourceID = string.Empty;
                        postedSourceID = string.Empty;
                        postedPackageLines = new List<PostedPackageLine>();
                    }
                }
            }

            return postPackage;
        }

        private List<PostedReceiveLine> ReturnPostedReceiveLines(string postedReceiveNo)
        {
            List<PostedReceiveLine> postReceiveLine = new List<PostedReceiveLine>();

            string itemNo = string.Empty;
            string serialNo = string.Empty;
            string receiveNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            string type = string.Empty;

            if (currResults.PostedReceiveLine != null)
            {
                for (int prl = 0; prl < currResults.PostedReceiveLine.Length; prl++)
                {
                    if (currResults.PostedReceiveLine[prl].RecNo == postedReceiveNo)
                    {
                        itemNo = currResults.PostedReceiveLine[prl].ItemNo;
                        serialNo = currResults.PostedReceiveLine[prl].SerialNo;
                        receiveNo = currResults.PostedReceiveLine[prl].RecNo;
                        description = currResults.PostedReceiveLine[prl].Description;
                        int.TryParse(currResults.PostedReceiveLine[prl].Qty, out quantity);
                        type = currResults.PostedReceiveLine[prl].Type;

                        postReceiveLine.Add(new PostedReceiveLine(itemNo, serialNo, receiveNo, description, quantity, type));
                    }
                }
            }

            return postReceiveLine;
        }

        private List<PostedReceive> ReturnPostedReceive(string returnOrderNo)
        {
            List<PostedReceive> postReceive = new List<PostedReceive>();

            string trackingNo = string.Empty;
            string packageNo = string.Empty;
            string shippingAgent = string.Empty;
            string shippingAgentService = string.Empty;
            string receiveDate = string.Empty;
            string sourceID = string.Empty;
            string postedSourceID = string.Empty;
            List<PostedReceiveLine> postedReceiveLines = new List<PostedReceiveLine>();

            if (currResults.PostedReceive != null)
            {
                for (int pr = 0; pr < currResults.PostedReceive.Length; pr++)
                {
                    if (currResults.PostedReceive[pr].SourceID == returnOrderNo)
                    {
                        trackingNo = currResults.PostedReceive[pr].ExtTrackNo;
                        packageNo = currResults.PostedReceive[pr].No;
                        shippingAgent = currResults.PostedReceive[pr].ShippingAgent;
                        shippingAgentService = currResults.PostedReceive[pr].ShippingService;
                        receiveDate = currResults.PostedReceive[pr].ReceiveDate;
                        sourceID = currResults.PostedReceive[pr].SourceID;
                        postedSourceID = currResults.PostedReceive[pr].PostedSourceID;
                        postedReceiveLines = ReturnPostedReceiveLines(packageNo);

                        postReceive.Add(new PostedReceive(trackingNo, packageNo, shippingAgent, shippingAgentService, receiveDate, sourceID, postedSourceID, postedReceiveLines));

                        trackingNo = string.Empty;
                        packageNo = string.Empty;
                        shippingAgent = string.Empty;
                        shippingAgentService = string.Empty;
                        receiveDate = string.Empty;
                        sourceID = string.Empty;
                        postedSourceID = string.Empty;
                        postedReceiveLines = new List<PostedReceiveLine>();
                    }
                }
            }

            return postReceive;
        }

        private List<ShipmentHeader> ReturnShipmentHeader(string orderNo)
        {
            List<ShipmentHeader> shipHeader = new List<ShipmentHeader>();

            string no = string.Empty;
            string externalDocumentNo = string.Empty;
            string shippingDate = string.Empty;
            string shippingAgentService = string.Empty;
            string shippingAgentCode = string.Empty;
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
                        shippingAgentCode = currResults.SalesShipmentHeader[sh].ShippingAgentCode;
                        shipLine = ReturnShipmentLines(no);

                        shipHeader.Add(new ShipmentHeader(no, externalDocumentNo, shippingDate, shippingAgentService, shippingAgentCode, shipLine));

                        no = string.Empty;
                        externalDocumentNo = string.Empty;
                        shippingDate = string.Empty;
                        shippingAgentService = string.Empty;
                        shippingAgentCode = string.Empty;
                        shipLine = new List<ShipmentLine>();
                    }
                }
            }
            return shipHeader;
        }

        private List<ShipmentLine> ReturnShipmentLines(string no)
        {
            List<ShipmentLine> shipLine = new List<ShipmentLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
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
                                int currQty;
                                int.TryParse(currResults.SalesShipmentLine[sli].Qty, out currQty);
                                quantityShipped += currQty;
                            }
                        }

                        shipLine.Add(new ShipmentLine(itemNo, description, quantity, quantityShipped, price, lineAmount));

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityShipped = 0;
                        price = 0;
                        lineAmount = 0;
                    }
                }
            }

            return shipLine;
        }

        private List<ReceiptLine> ReturnReceiptLines(string no)
        {
            List<ReceiptLine> receiptLine = new List<ReceiptLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityReceived = 0;
            double price = 0;
            double lineAmount = 0;

            if (currResults.ReturnReceiptLine != null)
            {
                for (int rl = 0; rl < currResults.ReturnReceiptLine.Length; rl++)
                {
                    if (currResults.ReturnReceiptLine[rl].DocNo == no)
                    {
                        itemNo = currResults.ReturnReceiptLine[rl].ItemNo;
                        description = currResults.ReturnReceiptLine[rl].Description;
                        int.TryParse(currResults.ReturnReceiptLine[rl].Qty, out quantity);
                        double.TryParse(currResults.ReturnReceiptLine[rl].UnitPrice, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        for (int rli = 0; rli < currResults.ReturnReceiptLine.Length; rli++)
                        {
                            if ((currResults.ReturnReceiptLine[rli].DocNo == no) && (currResults.ReturnReceiptLine[rli].ItemNo == itemNo))
                            {
                                int currQty;
                                int.TryParse(currResults.ReturnReceiptLine[rli].Qty, out currQty);
                                quantityReceived += currQty;
                            }
                        }

                        receiptLine.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount));

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityReceived = 0;
                        price = 0;
                        lineAmount = 0;
                    }
                }
            }

            return receiptLine;
        }

        private List<ReceiptHeader> ReturnReceiptHeader(string returnOrderNo)
        {
            List<ReceiptHeader> receiptHead = new List<ReceiptHeader>();

            string no = string.Empty;
            string externalDocumentNo = string.Empty;
            string receiptDate = string.Empty;
            List<ReceiptLine> receiptLines = new List<ReceiptLine>();

            if (currResults.ReturnReceiptHeader != null)
            {
                for (int rh = 0; rh < currResults.ReturnReceiptHeader.Length; rh++)
                {
                    if (currResults.ReturnReceiptHeader[rh].ReturnOrderNo == returnOrderNo)
                    {
                        no = currResults.ReturnReceiptHeader[rh].No;
                        externalDocumentNo = currResults.ReturnReceiptHeader[rh].ExtDocNo;
                        receiptDate = currResults.ReturnReceiptHeader[rh].ReceiveDate;
                        receiptLines = ReturnReceiptLines(no);

                        receiptHead.Add(new ReceiptHeader(no, externalDocumentNo, receiptDate, receiptLines));

                        no = string.Empty;
                        externalDocumentNo = string.Empty;
                        receiptDate = string.Empty;
                        receiptLines = new List<ReceiptLine>();
                    }
                }
            }

            return receiptHead;
        }

        public List<ReturnHeader> GetReturnOrders(string custName)
        {
            List<ReturnHeader> returnHead = new List<ReturnHeader>();

            string returnStatus = string.Empty;
            string dateCreated = string.Empty;
            string channelName = string.Empty;
            List<ReceiptHeader> receiptHeader = new List<ReceiptHeader>();
            List<PostedReceive> postedReceive = new List<PostedReceive>();
            string returnTrackingNo = string.Empty;
            string orderDate = string.Empty;
            string rmaNo = string.Empty;
            string externalDocumentNo = string.Empty;

            List<string> insertedReturnNumners = new List<string>();

            int statusCounter = 0;
            int totalCounter = 0;

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    if (currResults.SOImportBuffer[so].OrderType == "Credit Memo")
                    {
                        if (currResults.SOImportBuffer[so].ShipToName == custName)
                        {
                            rmaNo = currResults.SOImportBuffer[so].SalesOrderNo;

                            if (!insertedReturnNumners.Any(order => order.Equals(rmaNo)))
                            {
                                returnStatus = currResults.SOImportBuffer[so].OrderStatus;
                                channelName = currResults.SOImportBuffer[so].ChannelName[0];
                                receiptHeader = ReturnReceiptHeader(rmaNo);
                                postedReceive = ReturnPostedReceive(rmaNo);
                                //returnTrackingNo = currResults.SOImportBuffer[so];
                                orderDate = currResults.SOImportBuffer[so].OrderDate;
                                externalDocumentNo = currResults.SOImportBuffer[so].ExternalDocumentNo;

                                for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                {
                                    if (currResults.SalesLine[sl].DocNo == rmaNo)
                                    {
                                        dateCreated = currResults.SalesLine[sl].DateCreated;
                                    }
                                }

                                returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate, rmaNo, externalDocumentNo));
                                insertedReturnNumners.Add(rmaNo);

                                returnStatus = string.Empty;
                                dateCreated = string.Empty;
                                channelName = string.Empty;
                                receiptHeader = new List<ReceiptHeader>();
                                postedReceive = new List<PostedReceive>();
                                returnTrackingNo = string.Empty;
                                orderDate = string.Empty;
                                rmaNo = string.Empty;
                                externalDocumentNo = string.Empty;
                            }
                        }
                    }
                }
                if (returnHead.Count > 0)
                return returnHead;
            }

            if (currResults.SalesHeader != null)
            {
                for (int so = 0; so < currResults.SalesHeader.Length; so++)
                {
                    if (currResults.SalesHeader[so].DocType == "Return Order")
                    {
                        if (currResults.SalesHeader[so].ShipToName == custName)
                        {
                            rmaNo = currResults.SalesHeader[so].No;
                            receiptHeader = ReturnReceiptHeader(rmaNo);
                            channelName = currResults.SalesHeader[so].SellToCustomerName;
                            postedReceive = ReturnPostedReceive(rmaNo);
                            returnTrackingNo = currResults.SalesHeader[so].ReturnTrackingNo;
                            orderDate = currResults.SalesHeader[so].DocDate;
                            externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;

                            for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                            {
                                if ((currResults.SalesLine[sl].DocNo == rmaNo) && (currResults.SalesLine[sl].Type == "Item"))
                                {
                                    totalCounter++;
                                    int qtyToRec;
                                    dateCreated = currResults.SalesLine[sl].DateCreated;
                                    int.TryParse(currResults.SalesLine[sl].QtyToReceive, out qtyToRec);
                                    if (qtyToRec > 0)
                                    {
                                        statusCounter++;
                                    }
                                }
                            }

                            if (statusCounter == totalCounter)
                            {
                                returnStatus = "Open";
                            }
                            else if (statusCounter == 0)
                            {
                                returnStatus = "Released";
                            }
                            else
                            {
                                returnStatus = "Partial Receipt";
                            }

                            returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate, rmaNo, externalDocumentNo));
                            insertedReturnNumners.Add(rmaNo);

                            returnStatus = string.Empty;
                            dateCreated = string.Empty;
                            channelName = string.Empty;
                            receiptHeader = new List<ReceiptHeader>();
                            postedReceive = new List<PostedReceive>();
                            returnTrackingNo = string.Empty;
                            orderDate = string.Empty;
                            rmaNo = string.Empty;
                            externalDocumentNo = string.Empty;

                            totalCounter = 0;
                            statusCounter = 0;
                        }
                    }
                }
                if (returnHead.Count > 0)
                return returnHead;
            }

            if (currResults.ReturnReceiptHeader != null)
            {
                for (int so = 0; so < currResults.ReturnReceiptHeader.Length; so++)
                {
                  
                        if (currResults.ReturnReceiptHeader[so].ShipToName == custName)
                        {
                            rmaNo = currResults.ReturnReceiptHeader[so].ReturnOrderNo;
                            receiptHeader = ReturnReceiptHeader(rmaNo);
                            channelName = currResults.ReturnReceiptHeader[so].SellToCustomerName;
                            postedReceive = ReturnPostedReceive(rmaNo);
                            //returnTrackingNo = currResults.ReturnReceiptHeader[so].ReturnTrackingNo;
                            orderDate = currResults.ReturnReceiptHeader[so].ReceiveDate;
                            externalDocumentNo = currResults.ReturnReceiptHeader[so].ExtDocNo;

                           
                                returnStatus = "Released";
                            

                            returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate, rmaNo, externalDocumentNo));
                            insertedReturnNumners.Add(rmaNo);

                            returnStatus = string.Empty;
                            dateCreated = string.Empty;
                            channelName = string.Empty;
                            receiptHeader = new List<ReceiptHeader>();
                            postedReceive = new List<PostedReceive>();
                            returnTrackingNo = string.Empty;
                            orderDate = string.Empty;
                            rmaNo = string.Empty;
                            externalDocumentNo = string.Empty;

                            totalCounter = 0;
                            statusCounter = 0;
                        }
                    
                }
            }

            return returnHead;
        }

        public List<SalesHeader> GetSalesOrders(string custName)
        {
            List<SalesHeader> salesHead = new List<SalesHeader>();

            string orderStatus = string.Empty;
            string orderDate = string.Empty;
            string orderNo = string.Empty;
            string channelName = string.Empty;
            List<ShipmentHeader> shipHeader = new List<ShipmentHeader>();
            List<PostedPackage> postPackage = new List<PostedPackage>();
            string externalDocumentNo = string.Empty;
            Warranty warranty = new Warranty();

            string status = string.Empty;
            string policy = string.Empty;
            string daysRemaining = string.Empty;

            List<string> insertedOrderNumbers = new List<string>();

            int statusCounter = 0;
            int totalCounter = 0;

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    if (currResults.SOImportBuffer[so].OrderType == "Sales Order")
                    {
                        if (currResults.SOImportBuffer[so].ShipToName == custName)
                        {
                            orderNo = currResults.SOImportBuffer[so].SalesOrderNo;

                            if (!insertedOrderNumbers.Any(order => order.Equals(orderNo)))
                            {
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

                                orderStatus = string.Empty;
                                orderDate = string.Empty;
                                orderNo = string.Empty;
                                channelName = string.Empty;
                                shipHeader = new List<ShipmentHeader>();
                                postPackage = new List<PostedPackage>();
                                externalDocumentNo = string.Empty;
                                warranty = new Warranty();

                                status = string.Empty;
                                policy = string.Empty;
                                daysRemaining = string.Empty;
                            }
                        }
                    }
                }
                if (salesHead.Count > 0)
                    return salesHead;
            }

            if (currResults.SalesHeader != null)
            {
                for (int so = 0; so < currResults.SalesHeader.Length; so++)
                {
                    if (currResults.SalesHeader[so].DocType == "Order")
                    {
                        if (currResults.SalesHeader[so].ShipToName == custName)
                        {
                            orderDate = currResults.SalesHeader[so].DocDate;
                            orderNo = currResults.SalesHeader[so].No;
                            channelName = currResults.SalesHeader[so].SellToCustomerName;
                            externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;
                            shipHeader = ReturnShipmentHeader(orderNo);
                            postPackage = ReturnPostedPackage(orderNo);

                            status = currResults.SalesHeader[so].Warranty2[0].Status2[0];
                            policy = currResults.SalesHeader[so].Warranty2[0].Policy2[0]; ;
                            daysRemaining = currResults.SalesHeader[so].Warranty2[0].DaysRemaining2[0];
                            warranty = new Warranty(status, policy, daysRemaining);

                            for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                            {
                                if ((currResults.SalesLine[sl].DocNo == orderNo) && (currResults.SalesLine[sl].Type == "Item"))
                                {
                                    totalCounter++;
                                    int.TryParse(currResults.SalesLine[sl].QtyToReceive, out int qtyToRec);
                                    if (qtyToRec > 0)
                                    {
                                        statusCounter++;
                                    }
                                }
                            }

                            if (statusCounter == totalCounter)
                            {
                                orderStatus = "OrderCreated";
                            }
                            else if (statusCounter == 0)
                            {
                                orderStatus = "Shipped";
                            }
                            else
                            {
                                orderStatus = "Partial Shipped";
                            }

                            salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty));

                            insertedOrderNumbers.Add(orderNo);

                            orderStatus = string.Empty;
                            orderDate = string.Empty;
                            orderNo = string.Empty;
                            channelName = string.Empty;
                            shipHeader = new List<ShipmentHeader>();
                            postPackage = new List<PostedPackage>();
                            externalDocumentNo = string.Empty;
                            warranty = new Warranty();

                            status = string.Empty;
                            policy = string.Empty;
                            daysRemaining = string.Empty;
                        }
                    }
                }
                if (salesHead.Count > 0)
                    return salesHead;
            }
            if (currResults.SalesShipmentHeader != null)
            {
                for (int so = 0; so < currResults.SalesShipmentHeader.Length; so++)
                {

                    if (currResults.SalesShipmentHeader[so].ShipToName == custName)
                    {
                        orderDate = currResults.SalesShipmentHeader[so].OrderDate;
                        orderNo = currResults.SalesShipmentHeader[so].OrderNo;
                        channelName = currResults.SalesShipmentHeader[so].ShipToName;
                        externalDocumentNo = currResults.SalesShipmentHeader[so].ExtDocNo;
                        shipHeader = ReturnShipmentHeader(orderNo);
                        postPackage = ReturnPostedPackage(orderNo);

                        status = currResults.SalesShipmentHeader[so].Warranty3[0].Status3[0];
                        policy = currResults.SalesShipmentHeader[so].Warranty3[0].Policy3[0]; ;
                        daysRemaining = currResults.SalesShipmentHeader[so].Warranty3[0].DaysRemaining3[0];
                        warranty = new Warranty(status, policy, daysRemaining);

                        orderStatus = "Shipped";

                        salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty));

                        insertedOrderNumbers.Add(orderNo);

                        orderStatus = string.Empty;
                        orderDate = string.Empty;
                        orderNo = string.Empty;
                        channelName = string.Empty;
                        shipHeader = new List<ShipmentHeader>();
                        postPackage = new List<PostedPackage>();
                        externalDocumentNo = string.Empty;
                        warranty = new Warranty();

                        status = string.Empty;
                        policy = string.Empty;
                        daysRemaining = string.Empty;
                    }

                }
            }

            return salesHead;
        }

        public List<Customer> GetCustomerInfo()
        {
            List<Customer> returnCust = new List<Customer>();

            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToZip = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;
            List<SalesHeader> salesHeaders = new List<SalesHeader>();
            List<ReturnHeader> returnHeaders = new List<ReturnHeader>();

            List<string> customerNames = new List<string>();

            if (currResults.SOImportBuffer != null)
            {
                for (int c = 0; c < currResults.SOImportBuffer.Length; c++)
                {
                    shipToName = currResults.SOImportBuffer[c].ShipToName;

                    if (!customerNames.Any(order => order.Equals(shipToName)))
                    {
                        shipToAddress1 = currResults.SOImportBuffer[c].ShipToAddress;
                        shipToAddress2 = currResults.SOImportBuffer[c].ShipToAddress2;
                        shipToContact = currResults.SOImportBuffer[c].ShipToContact;
                        shipToCity = currResults.SOImportBuffer[c].ShipToCity;
                        shipToZip = currResults.SOImportBuffer[c].ShipToZip;
                        shipToState = currResults.SOImportBuffer[c].ShipToState;
                        shipToCountry = currResults.SOImportBuffer[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName);
                        returnHeaders = GetReturnOrders(shipToName);

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);

                        shipToName = string.Empty;
                        shipToAddress1 = string.Empty;
                        shipToAddress2 = string.Empty;
                        shipToContact = string.Empty;
                        shipToCity = string.Empty;
                        shipToZip = string.Empty;
                        shipToState = string.Empty;
                        shipToCountry = string.Empty;

                        salesHeaders = new List<SalesHeader>();
                        returnHeaders = new List<ReturnHeader>();
                    }
                }
            }

            if (currResults.SalesShipmentHeader != null)
            {
                for (int c = 0; c < currResults.SalesShipmentHeader.Length; c++)
                {
                    shipToName = currResults.SalesShipmentHeader[c].ShipToName;

                    if (!customerNames.Any(order => order.Equals(shipToName)))
                    {
                        shipToAddress1 = currResults.SalesShipmentHeader[c].ShipToAddress;
                        shipToAddress2 = currResults.SalesShipmentHeader[c].ShipToAddress2;
                        shipToContact = currResults.SalesShipmentHeader[c].ShipToContact;
                        shipToCity = currResults.SalesShipmentHeader[c].ShipToCity;
                        shipToZip = currResults.SalesShipmentHeader[c].ShipToZip;
                        shipToState = currResults.SalesShipmentHeader[c].ShipToState;
                        shipToCountry = currResults.SalesShipmentHeader[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName);
                        returnHeaders = GetReturnOrders(shipToName);

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);

                        shipToName = string.Empty;
                        shipToAddress1 = string.Empty;
                        shipToAddress2 = string.Empty;
                        shipToContact = string.Empty;
                        shipToCity = string.Empty;
                        shipToZip = string.Empty;
                        shipToState = string.Empty;
                        shipToCountry = string.Empty;

                        salesHeaders = new List<SalesHeader>();
                        returnHeaders = new List<ReturnHeader>();
                    }
                }
            }

            if (currResults.SalesHeader != null)
            {
                for (int c = 0; c < currResults.SalesHeader.Length; c++)
                {
                    shipToName = currResults.SalesHeader[c].ShipToName;

                    if (!customerNames.Any(order => order.Equals(shipToName)))
                    {
                        shipToAddress1 = currResults.SalesHeader[c].ShipToAddress;
                        shipToAddress2 = currResults.SalesHeader[c].ShipToAddress2;
                        shipToContact = currResults.SalesHeader[c].ShipToContact;
                        shipToCity = currResults.SalesHeader[c].ShipToCity;
                        shipToZip = currResults.SalesHeader[c].ShipToZip;
                        shipToState = currResults.SalesHeader[c].ShipToState;
                        shipToCountry = currResults.SalesHeader[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName);
                        returnHeaders = GetReturnOrders(shipToName);

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);

                        shipToName = string.Empty;
                        shipToAddress1 = string.Empty;
                        shipToAddress2 = string.Empty;
                        shipToContact = string.Empty;
                        shipToCity = string.Empty;
                        shipToZip = string.Empty;
                        shipToState = string.Empty;
                        shipToCountry = string.Empty;

                        salesHeaders = new List<SalesHeader>();
                        returnHeaders = new List<ReturnHeader>();
                    }
                }
            }

            if (currResults.ReturnReceiptHeader != null)
            {
                for (int c = 0; c < currResults.ReturnReceiptHeader.Length; c++)
                {
                    shipToName = currResults.ReturnReceiptHeader[c].ShipToName;

                    if (!customerNames.Any(order => order.Equals(shipToName)))
                    {
                        shipToAddress1 = currResults.ReturnReceiptHeader[c].ShipToAddress;
                        shipToAddress2 = currResults.ReturnReceiptHeader[c].ShipToAddress2;
                        shipToContact = currResults.ReturnReceiptHeader[c].ShipToContact;
                        shipToCity = currResults.ReturnReceiptHeader[c].ShipToCity;
                        shipToZip = currResults.ReturnReceiptHeader[c].ShipToZip;
                        shipToState = currResults.ReturnReceiptHeader[c].ShipToState;
                        shipToCountry = currResults.ReturnReceiptHeader[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName);
                        returnHeaders = GetReturnOrders(shipToName);

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);

                        shipToName = string.Empty;
                        shipToAddress1 = string.Empty;
                        shipToAddress2 = string.Empty;
                        shipToContact = string.Empty;
                        shipToCity = string.Empty;
                        shipToZip = string.Empty;
                        shipToState = string.Empty;
                        shipToCountry = string.Empty;

                        salesHeaders = new List<SalesHeader>();
                        returnHeaders = new List<ReturnHeader>();
                    }
                }
            }

            return returnCust;
        }
    }
}