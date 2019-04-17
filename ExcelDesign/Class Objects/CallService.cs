using System.Collections.Generic;
using System.Linq;
using ExcelDesign.ServiceFunctions;
using System.Globalization;
using ExcelDesign.Class_Objects.FunctionData;
using System.Web;
using System;
using ExcelDesign.ZendeskAPI;

namespace ExcelDesign.Class_Objects
{
    /* 16 October 2018 - Neil Jansen
    * Updated logic to not match on external document no.s, but to loop through the extended sales header as we have updated the logic to link Sales Orders and Return Orders through this record.
    */

    /* V10 - 28 March 2019 - Neil Jansen
     * Added Cross-Reference No from Sales Line
     */

    /* v7.1 - 3 October 2018 - Neil Jansen
     * GetStatisticsInformation() - Added older than 24 hours bucket
     */

    /* v7.2 - 12 October 2018 - Neil Jansen
     * Added Customer Allow Refund property logic in GetStatisticsInformation()
     * Updated logic for GetReturnOrdersFromSalesHeader()
     */

    public class CallService
    {
        private WebService webService;
        SearchResults currResults = new SearchResults();

        public CallService()
        {
            webService = new WebService();
        }

        public User LoginUser(string userID, string password)
        {
            return webService.UserLogin(userID, password);
        }

        public void OpenService(string searchNo, int searchOption)
        {
            currResults = webService.FindOrder(searchNo, searchOption);
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
                        double.TryParse(currResults.PostedPackageLine[ppl].Price.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);

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

        private List<PostedPackage> ReturnPostedPackage(string orderNo, string shipmentNo)
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
                    if (currResults.PostedPackage[pp].SourceID == orderNo &&
                        currResults.PostedPackage[pp].PostedSourceID == shipmentNo)
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

        private List<PostedReceive> ReturnPostedReceive(string rmaNo, string returnReceiptNo)
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
                    if (currResults.PostedReceive[pr].PostedSourceID == returnReceiptNo &&
                        currResults.PostedReceive[pr].SourceID == rmaNo)
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
            string sellToCustomerNo = string.Empty;
            List<ShipmentLine> shipLine = new List<ShipmentLine>();
            List<ReceiptLine> returnLine = new List<ReceiptLine>();
            List<string> rmaNo = new List<string>();
            List<string> orderNoList = new List<string>();

            if (currResults.SalesShipmentHeader != null)
            {
                for (int sh = 0; sh < currResults.SalesShipmentHeader.Length; sh++)
                {
                    if (currResults.SalesShipmentHeader[sh].OrderNo == orderNo)
                    {
                        no = currResults.SalesShipmentHeader[sh].No;

                        if (!orderNoList.Any(order => order.Equals(no)))
                        {
                            externalDocumentNo = currResults.SalesShipmentHeader[sh].ExtDocNo;
                            shippingDate = currResults.SalesShipmentHeader[sh].ShippingDate;
                            shippingAgentService = currResults.SalesShipmentHeader[sh].ShippingAgentService;
                            shippingAgentCode = currResults.SalesShipmentHeader[sh].ShippingAgentCode;
                            shipLine = ReturnShipmentLines(no);
                            sellToCustomerNo = currResults.SalesShipmentHeader[sh].SellToCustomerNo;
                            returnLine = GetShipmentReturnLines(no);

                            if (currResults.ExtendedSalesHeader != null)
                            {
                                for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                {
                                    if (currResults.ExtendedSalesHeader[esh].SSHNo == no)
                                    {
                                        rmaNo.Add(currResults.ExtendedSalesHeader[esh].RMANo);
                                    }
                                }
                            }

                            shipHeader.Add(new ShipmentHeader(no, externalDocumentNo, shippingDate, shippingAgentService, shippingAgentCode, shipLine, sellToCustomerNo, returnLine, rmaNo, false));
                            orderNoList.Add(orderNo);

                            no = string.Empty;
                            externalDocumentNo = string.Empty;
                            shippingDate = string.Empty;
                            shippingAgentService = string.Empty;
                            shippingAgentCode = string.Empty;
                            sellToCustomerNo = string.Empty;
                            shipLine = new List<ShipmentLine>();
                            returnLine = new List<ReceiptLine>();
                            rmaNo = new List<string>();
                        }
                    }
                }
            }

            if (currResults.SalesHeader != null)
            {
                for (int sh = 0; sh < currResults.SalesHeader.Length; sh++)
                {
                    if (currResults.SalesHeader[sh].No == orderNo && (currResults.SalesHeader[sh].DocType == "Order" || currResults.SalesHeader[sh].DocType == "Quote"))
                    {
                        no = currResults.SalesHeader[sh].No;

                        if (!orderNoList.Any(order => order.Equals(no)))
                        {
                            externalDocumentNo = currResults.SalesHeader[sh].ExtDocNo;
                            shippingDate = currResults.SalesHeader[sh].DocDate;
                            //shippingAgentService = currResults.SalesHeader[sh].;
                            //shippingAgentCode = currResults.SalesHeader[sh].ag;
                            shipLine = ReturnSalesShipmentLine(no);
                            //sellToCustomerNo = currResults.SalesHeader[sh].SellToCustomerNo;
                            returnLine = GetShipmentReturnLines(no);

                            shipHeader.Add(new ShipmentHeader(no, externalDocumentNo, shippingDate, shippingAgentService, shippingAgentCode, shipLine, sellToCustomerNo, returnLine, rmaNo, true));

                            no = string.Empty;
                            externalDocumentNo = string.Empty;
                            shippingDate = string.Empty;
                            shippingAgentService = string.Empty;
                            shippingAgentCode = string.Empty;
                            sellToCustomerNo = string.Empty;
                            shipLine = new List<ShipmentLine>();
                            returnLine = new List<ReceiptLine>();
                            rmaNo = new List<string>();
                        }
                    }
                }
            }

            return shipHeader;
        }

        private List<ShipmentLine> ReturnSalesShipmentLine(string no)
        {
            List<ShipmentLine> shipLine = new List<ShipmentLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityShipped = 0;
            double price = 0;
            double lineAmount = 0;
            string type = string.Empty;
            string crossRefNo = string.Empty;

            List<string> insertedItems = new List<string>();

            if (currResults.SalesLine != null)
            {
                for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                {
                    if (currResults.SalesLine[sl].DocNo == no)
                    {
                        itemNo = currResults.SalesLine[sl].ItemNo;
                        description = currResults.SalesLine[sl].Description;
                        int.TryParse(currResults.SalesLine[sl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                        double.TryParse(currResults.SalesLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;
                        type = currResults.SalesLine[sl].Type;
                        crossRefNo = currResults.SalesLine[sl].CrossRefNo;

                        if (insertedItems.Any(item => item.Equals(itemNo)) && insertedItems.Any(desc => desc.Equals(description)))
                        {
                            foreach (ShipmentLine existingItem in shipLine)
                            {
                                if (existingItem.ItemNo == itemNo)
                                {
                                    existingItem.Quantity += quantity;
                                    existingItem.LineAmount = existingItem.Quantity * price;
                                }
                            }
                        }
                        else
                        {
                            shipLine.Add(new ShipmentLine(itemNo, description, quantity, quantityShipped, price, lineAmount, type, crossRefNo));
                            insertedItems.Add(itemNo);
                            insertedItems.Add(description);
                        }

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityShipped = 0;
                        price = 0;
                        lineAmount = 0;
                        crossRefNo = string.Empty;
                    }
                }
            }

            return shipLine;
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
            string type = string.Empty;
            List<string> insertedItems = new List<string>();
            string crossRefNo = string.Empty;


            if (currResults.SalesShipmentLine != null)
            {
                for (int sl = 0; sl < currResults.SalesShipmentLine.Length; sl++)
                {
                    if (currResults.SalesShipmentLine[sl].DocNo == no)
                    {
                        itemNo = currResults.SalesShipmentLine[sl].ItemNo;
                        description = currResults.SalesShipmentLine[sl].Description;
                        int.TryParse(currResults.SalesShipmentLine[sl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                        double.TryParse(currResults.SalesShipmentLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;
                        type = currResults.SalesShipmentLine[sl].Type;
                        crossRefNo = currResults.SalesShipmentLine[sl].CrossRefNo;

                        if (insertedItems.Any(item => item.Equals(itemNo)) && insertedItems.Any(desc => desc.Equals(description)))
                        {
                            foreach (ShipmentLine existingItem in shipLine)
                            {
                                if (existingItem.ItemNo == itemNo)
                                {
                                    existingItem.Quantity += quantity;
                                    existingItem.LineAmount = existingItem.Quantity * price;
                                }
                            }
                        }
                        else
                        {
                            for (int sli = 0; sli < currResults.SalesShipmentLine.Length; sli++)
                            {
                                if ((currResults.SalesShipmentLine[sli].DocNo == no) && (currResults.SalesShipmentLine[sli].ItemNo == itemNo))
                                {
                                    int.TryParse(currResults.SalesShipmentLine[sli].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out int currQty);
                                    quantityShipped += currQty;
                                }
                            }

                            shipLine.Add(new ShipmentLine(itemNo, description, quantity, quantityShipped, price, lineAmount, type, crossRefNo));
                            insertedItems.Add(itemNo);
                            insertedItems.Add(description);
                        }

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityShipped = 0;
                        price = 0;
                        lineAmount = 0;
                        crossRefNo = string.Empty;
                    }
                }
            }

            return shipLine;
        }

        private List<ReceiptLine> ReturnReceiptLines(string no, string returnOrderNo)
        {
            List<ReceiptLine> receiptLine = new List<ReceiptLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityReceived = 0;
            double price = 0;
            double lineAmount = 0;
            int quantityExchanged = 0;
            int quantityRefunded = 0;
            string reqReturnAction = string.Empty;
            string returnReason = string.Empty;
            string crossRefNo = string.Empty;
            List<string> insertedItems = new List<string>();

            if (currResults.ReturnReceiptLine != null)
            {
                for (int rl = 0; rl < currResults.ReturnReceiptLine.Length; rl++)
                {
                    if (currResults.ReturnReceiptLine[rl].DocNo == no)
                    {
                        itemNo = currResults.ReturnReceiptLine[rl].ItemNo;
                        description = currResults.ReturnReceiptLine[rl].Description;
                        int.TryParse(currResults.ReturnReceiptLine[rl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                        double.TryParse(currResults.ReturnReceiptLine[rl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;
                        crossRefNo = currResults.ReturnReceiptLine[rl].CrossRefNo;

                        if (insertedItems.Any(item => item.Equals(itemNo)))
                        {
                            foreach (ReceiptLine existingItem in receiptLine)
                            {
                                if (existingItem.ItemNo == itemNo)
                                {
                                    existingItem.Quantity += quantity;
                                    existingItem.LineAmount = existingItem.Quantity * price;
                                }
                            }
                        }
                        else
                        {
                            for (int rli = 0; rli < currResults.ReturnReceiptLine.Length; rli++)
                            {
                                if ((currResults.ReturnReceiptLine[rli].DocNo == no) && (currResults.ReturnReceiptLine[rli].ItemNo == itemNo))
                                {
                                    int.TryParse(currResults.ReturnReceiptLine[rli].Qty, out int currQty);
                                    quantityReceived += currQty;
                                }
                            }

                            if (currResults.SalesLine != null)
                            {
                                for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                {
                                    if ((currResults.SalesLine[sl].DocNo == returnOrderNo) && (currResults.SalesLine[sl].ItemNo == itemNo))
                                    {
                                        quantityExchanged = (int)Convert.ToDouble(currResults.SalesLine[sl].QtyExchanged, CultureInfo.InvariantCulture.NumberFormat);
                                        quantityRefunded = (int)Convert.ToDouble(currResults.SalesLine[sl].QtyRefunded, CultureInfo.InvariantCulture.NumberFormat);
                                        reqReturnAction = currResults.SalesLine[sl].REQReturnAction;
                                        returnReason = currResults.SalesLine[sl].ReturnReason;
                                    }
                                }
                            }
                            else
                            {
                                if (currResults.SalesCreditMemo != null)
                                {
                                    for (int scm = 0; scm < currResults.SalesCreditMemo.Length; scm++)
                                    {
                                        if (currResults.SalesCreditMemo[scm].ReturnOrderNo == returnOrderNo)
                                        {
                                            if (currResults.SalesCreditMemoLines != null)
                                            {
                                                returnReason = currResults.ReturnReceiptLine[rl].ReturnReasonCode;
                                                reqReturnAction = currResults.ReturnReceiptLine[rl].REQReturnAction;

                                                for (int scml = 0; scml < currResults.SalesCreditMemoLines.Length; scml++)
                                                {
                                                    if ((currResults.SalesCreditMemoLines[scml].DocNo == currResults.SalesCreditMemo[scm].No) && (currResults.SalesCreditMemoLines[scml].ItemNo == itemNo))
                                                    {
                                                        quantityExchanged = (int)Convert.ToDouble(currResults.SalesCreditMemoLines[scml].QtyExchanged, CultureInfo.InvariantCulture.NumberFormat);
                                                        quantityRefunded = (int)Convert.ToDouble(currResults.SalesCreditMemoLines[scml].QtyRefunded, CultureInfo.InvariantCulture.NumberFormat);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            receiptLine.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount, quantityExchanged, quantityRefunded, reqReturnAction, returnReason, crossRefNo));
                            insertedItems.Add(itemNo);
                        }

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityReceived = 0;
                        price = 0;
                        lineAmount = 0;
                        quantityExchanged = 0;
                        quantityRefunded = 0;
                        reqReturnAction = string.Empty;
                        returnReason = string.Empty;
                        crossRefNo = string.Empty;
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
            string shippingAgentCode = string.Empty;
            string shippingAgentService = string.Empty;

            if (currResults.ReturnReceiptHeader != null)
            {
                for (int rh = 0; rh < currResults.ReturnReceiptHeader.Length; rh++)
                {
                    if (currResults.ReturnReceiptHeader[rh].ReturnOrderNo == returnOrderNo)
                    {
                        no = currResults.ReturnReceiptHeader[rh].No;
                        externalDocumentNo = currResults.ReturnReceiptHeader[rh].ExtDocNo;
                        receiptDate = currResults.ReturnReceiptHeader[rh].ReceiveDate;
                        receiptLines = ReturnReceiptLines(no, returnOrderNo);
                        shippingAgentCode = currResults.ReturnReceiptHeader[rh].ShippingAgent;

                        receiptHead.Add(new ReceiptHeader(no, externalDocumentNo, receiptDate, receiptLines, shippingAgentCode, false));

                        no = string.Empty;
                        externalDocumentNo = string.Empty;
                        receiptDate = string.Empty;
                        receiptLines = new List<ReceiptLine>();
                        shippingAgentCode = string.Empty;
                        shippingAgentService = string.Empty;
                    }
                }
            }

            return receiptHead;
        }

        public List<ReturnHeader> GetReturnOrders(string custName, string shipAddress, ref List<string> readRMA)
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
            string email = string.Empty;
            bool returnLabelCreated = false;
            bool exchangeCreated = false;
            List<string> exchangeOrderNo = new List<string>();
            string sellToCustomerNo = string.Empty;
            List<Comment> commentLines = new List<Comment>();
            string imeiNo = string.Empty;

            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToCode = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;

            List<string> insertedReturnNumbners = new List<string>();

            int statusCounter = 0;

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    if (currResults.SOImportBuffer[so].OrderType == "Credit Memo")
                    {
                        if (currResults.SOImportBuffer[so].ShipToName.ToUpper() == custName && currResults.SOImportBuffer[so].ShipToAddress.ToUpper() == shipAddress)
                        {
                            rmaNo = currResults.SOImportBuffer[so].SalesOrderNo;

                            if (!readRMA.Any(rma => rma.Equals(rmaNo)))
                            {
                                if (!insertedReturnNumbners.Any(order => order.Equals(rmaNo)))
                                {
                                    returnStatus = currResults.SOImportBuffer[so].OrderStatus;
                                    channelName = currResults.SOImportBuffer[so].ChannelName[0];
                                    receiptHeader = ReturnReceiptHeader(rmaNo);

                                    foreach (ReceiptHeader rh in receiptHeader)
                                    {
                                        postedReceive.AddRange(ReturnPostedReceive(rmaNo, rh.No));
                                    }

                                    orderDate = currResults.SOImportBuffer[so].OrderDate;
                                    externalDocumentNo = currResults.SOImportBuffer[so].ExternalDocumentNo;
                                    sellToCustomerNo = currResults.SOImportBuffer[so].CustomerNo;

                                    for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                    {
                                        if (currResults.SalesLine[sl].DocNo == rmaNo)
                                        {
                                            dateCreated = currResults.SalesLine[sl].DateCreated;
                                        }
                                    }

                                    if (currResults.ExtendedSalesHeader != null)
                                    {
                                        for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                        {
                                            if (currResults.ExtendedSalesHeader[esh].RMANo == rmaNo)
                                            {
                                                email = currResults.ExtendedSalesHeader[esh].Email;

                                                if (currResults.ExtendedSalesHeader[esh].IsRefund.ToUpper() == "YES")
                                                {
                                                    returnStatus = currResults.ExtendedSalesHeader[esh].RefundStatus;
                                                }
                                            }
                                        }
                                    }

                                    shipToName = currResults.SalesHeader[so].ShipToName;
                                    shipToAddress1 = currResults.SalesHeader[so].ShipToAddress;
                                    shipToAddress2 = currResults.SalesHeader[so].ShipToAddress2;
                                    shipToContact = currResults.SalesHeader[so].ShipToContact;
                                    shipToCity = currResults.SalesHeader[so].ShipToCity;
                                    shipToCode = currResults.SalesHeader[so].ShipToZip;
                                    shipToState = currResults.SalesHeader[so].ShipToState;
                                    shipToCountry = currResults.SalesHeader[so].ShipToCountry;

                                    // 9 November 2018 - Updated logic to pull comments using SSH no aswell
                                    commentLines.AddRange(GetSalesLineComments(rmaNo));
                                    foreach (ReceiptHeader rh in receiptHeader)
                                    {
                                        commentLines.AddRange(GetSalesLineComments(rh.No));
                                    }
                                    returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo,
                                        orderDate, rmaNo, externalDocumentNo, email, false, exchangeCreated, exchangeOrderNo, sellToCustomerNo, commentLines, imeiNo,
                                        shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToCode, shipToState, shipToCountry));

                                    insertedReturnNumbners.Add(rmaNo);
                                    readRMA.Add(rmaNo);

                                    returnStatus = string.Empty;
                                    dateCreated = string.Empty;
                                    channelName = string.Empty;
                                    receiptHeader = new List<ReceiptHeader>();
                                    postedReceive = new List<PostedReceive>();
                                    returnTrackingNo = string.Empty;
                                    orderDate = string.Empty;
                                    rmaNo = string.Empty;
                                    externalDocumentNo = string.Empty;
                                    email = string.Empty;
                                    returnLabelCreated = false;
                                    exchangeCreated = false;
                                    exchangeOrderNo = new List<string>();
                                    sellToCustomerNo = string.Empty;
                                    commentLines = new List<Comment>();
                                    imeiNo = string.Empty;

                                    shipToName = string.Empty;
                                    shipToAddress1 = string.Empty;
                                    shipToAddress2 = string.Empty;
                                    shipToContact = string.Empty;
                                    shipToCity = string.Empty;
                                    shipToCode = string.Empty;
                                    shipToState = string.Empty;
                                    shipToCountry = string.Empty;

                                    statusCounter = 0;
                                }
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
                        if (currResults.SalesHeader[so].ShipToName.ToUpper() == custName && currResults.SalesHeader[so].ShipToAddress.ToUpper() == shipAddress)
                        {
                            rmaNo = currResults.SalesHeader[so].No;

                            if (!readRMA.Any(rma => rma.Equals(rmaNo)))
                            {
                                if (!insertedReturnNumbners.Any(order => order.Equals(rmaNo)))
                                {
                                    receiptHeader = ReturnReceiptHeader(rmaNo);
                                    channelName = currResults.SalesHeader[so].SellToCustomerName;
                                    returnLabelCreated = currResults.SalesHeader[so].UPSRetLabelCreated.ToUpper() == "YES" ? true : false;

                                    if (currResults.SalesHeader[so].RMANo != "")
                                    {
                                        exchangeCreated = true;

                                        if (currResults.SalesHeader[so].ExchangeOrderNos[0].Contains("|"))
                                        {
                                            string[] tempSplit = currResults.SalesHeader[so].ExchangeOrderNos[0].Split('|');

                                            for (int i = 0; i < tempSplit.Length; i++)
                                            {
                                                exchangeOrderNo.Add(tempSplit[i]);
                                            }
                                        }
                                        else
                                        {
                                            exchangeOrderNo.Add(currResults.SalesHeader[so].ExchangeOrderNos[0]);
                                        }
                                    }

                                    foreach (ReceiptHeader rh in receiptHeader)
                                    {
                                        postedReceive.AddRange(ReturnPostedReceive(rmaNo, rh.No));
                                    }

                                    returnTrackingNo = currResults.SalesHeader[so].ReturnTrackingNo;
                                    orderDate = currResults.SalesHeader[so].OrderDate;
                                    externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;
                                    sellToCustomerNo = currResults.SalesHeader[so].SellToCustomerNo;
                                    imeiNo = currResults.SalesHeader[so].IMEI;
                                    int totalCounter = 0;

                                    for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                    {
                                        if ((currResults.SalesLine[sl].DocNo == rmaNo) && (currResults.SalesLine[sl].Type == "Item"))
                                        {
                                            totalCounter++;
                                            dateCreated = currResults.SalesLine[sl].DateCreated;
                                            int.TryParse(currResults.SalesLine[sl].QtyToReceive, out int qtyToRec);
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
                                        returnStatus = "Received";
                                    }
                                    else
                                    {
                                        returnStatus = "Partial Received";
                                    }

                                    if (currResults.ExtendedSalesHeader != null)
                                    {
                                        for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                        {
                                            if (currResults.ExtendedSalesHeader[esh].RMANo == rmaNo)
                                            {
                                                email = currResults.ExtendedSalesHeader[esh].Email;

                                                if (currResults.ExtendedSalesHeader[esh].IsRefund.ToUpper() == "YES")
                                                {
                                                    returnStatus = currResults.ExtendedSalesHeader[esh].RefundStatus;
                                                }
                                            }
                                        }
                                    }

                                    shipToName = currResults.SalesHeader[so].ShipToName;
                                    shipToAddress1 = currResults.SalesHeader[so].ShipToAddress;
                                    shipToAddress2 = currResults.SalesHeader[so].ShipToAddress2;
                                    shipToContact = currResults.SalesHeader[so].ShipToContact;
                                    shipToCity = currResults.SalesHeader[so].ShipToCity;
                                    shipToCode = currResults.SalesHeader[so].ShipToZip;
                                    shipToState = currResults.SalesHeader[so].ShipToState;
                                    shipToCountry = currResults.SalesHeader[so].ShipToCountry;

                                    //9 November 2018 - Updated logic to pull comments using Receipt No aswell
                                    commentLines.AddRange(GetSalesLineComments(rmaNo));
                                    foreach (ReceiptHeader rh in receiptHeader)
                                    {
                                        commentLines.AddRange(GetSalesLineComments(rh.No));
                                    }
                                    returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate,
                                        rmaNo, externalDocumentNo, email, returnLabelCreated, exchangeCreated, exchangeOrderNo, sellToCustomerNo, commentLines, imeiNo,
                                        shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToCode, shipToState, shipToCountry));
                                    insertedReturnNumbners.Add(rmaNo);
                                    readRMA.Add(rmaNo);

                                    returnStatus = string.Empty;
                                    dateCreated = string.Empty;
                                    channelName = string.Empty;
                                    receiptHeader = new List<ReceiptHeader>();
                                    postedReceive = new List<PostedReceive>();
                                    returnTrackingNo = string.Empty;
                                    orderDate = string.Empty;
                                    rmaNo = string.Empty;
                                    externalDocumentNo = string.Empty;
                                    email = string.Empty;
                                    returnLabelCreated = false;
                                    exchangeCreated = false;
                                    exchangeOrderNo = new List<string>();
                                    sellToCustomerNo = string.Empty;
                                    commentLines = new List<Comment>();
                                    imeiNo = string.Empty;

                                    shipToName = string.Empty;
                                    shipToAddress1 = string.Empty;
                                    shipToAddress2 = string.Empty;
                                    shipToContact = string.Empty;
                                    shipToCity = string.Empty;
                                    shipToCode = string.Empty;
                                    shipToState = string.Empty;
                                    shipToCountry = string.Empty;

                                    totalCounter = 0;
                                    statusCounter = 0;
                                }
                            }
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
                    if (currResults.ReturnReceiptHeader[so].ShipToName.ToUpper() == custName && currResults.ReturnReceiptHeader[so].ShipToAddress.ToUpper() == shipAddress)
                    {
                        rmaNo = currResults.ReturnReceiptHeader[so].ReturnOrderNo;

                        if (!readRMA.Any(rma => rma.Equals(rmaNo)))
                        {
                            if (!insertedReturnNumbners.Any(order => order.Equals(rmaNo)))
                            {
                                dateCreated = currResults.ReturnReceiptHeader[so].ReceiveDate;
                                receiptHeader = ReturnReceiptHeader(rmaNo);
                                channelName = currResults.ReturnReceiptHeader[so].SellToCustomerName;

                                foreach (ReceiptHeader rh in receiptHeader)
                                {
                                    postedReceive.AddRange(ReturnPostedReceive(rmaNo, rh.No));
                                }

                                orderDate = currResults.ReturnReceiptHeader[so].OrderDate;
                                externalDocumentNo = currResults.ReturnReceiptHeader[so].ExtDocNo;
                                sellToCustomerNo = currResults.ReturnReceiptHeader[so].SellToCustomerNo;

                                returnStatus = "Received";

                                if (currResults.ExtendedSalesHeader != null)
                                {
                                    for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                    {
                                        if (currResults.ExtendedSalesHeader[esh].RMANo == rmaNo)
                                        {
                                            email = currResults.ExtendedSalesHeader[esh].Email;

                                            if (currResults.ExtendedSalesHeader[esh].IsRefund.ToUpper() == "YES")
                                            {
                                                returnStatus = currResults.ExtendedSalesHeader[esh].RefundStatus;
                                            }
                                        }
                                    }
                                }

                                shipToName = currResults.SalesHeader[so].ShipToName;
                                shipToAddress1 = currResults.SalesHeader[so].ShipToAddress;
                                shipToAddress2 = currResults.SalesHeader[so].ShipToAddress2;
                                shipToContact = currResults.SalesHeader[so].ShipToContact;
                                shipToCity = currResults.SalesHeader[so].ShipToCity;
                                shipToCode = currResults.SalesHeader[so].ShipToZip;
                                shipToState = currResults.SalesHeader[so].ShipToState;
                                shipToCountry = currResults.SalesHeader[so].ShipToCountry;

                                //9 November 2018 - Updated logic to pull comments using Receipt No aswell
                                commentLines.AddRange(GetSalesLineComments(rmaNo));
                                foreach (ReceiptHeader rh in receiptHeader)
                                {
                                    commentLines.AddRange(GetSalesLineComments(rh.No));
                                }
                                returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate,
                                    rmaNo, externalDocumentNo, email, false, exchangeCreated, exchangeOrderNo, sellToCustomerNo, commentLines, imeiNo,
                                        shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToCode, shipToState, shipToCountry));
                                insertedReturnNumbners.Add(rmaNo);
                                readRMA.Add(rmaNo);

                                returnStatus = string.Empty;
                                dateCreated = string.Empty;
                                channelName = string.Empty;
                                receiptHeader = new List<ReceiptHeader>();
                                postedReceive = new List<PostedReceive>();
                                returnTrackingNo = string.Empty;
                                orderDate = string.Empty;
                                rmaNo = string.Empty;
                                externalDocumentNo = string.Empty;
                                email = string.Empty;
                                exchangeCreated = false;
                                exchangeOrderNo = new List<string>();
                                commentLines = new List<Comment>();
                                imeiNo = string.Empty;

                                shipToName = string.Empty;
                                shipToAddress1 = string.Empty;
                                shipToAddress2 = string.Empty;
                                shipToContact = string.Empty;
                                shipToCity = string.Empty;
                                shipToCode = string.Empty;
                                shipToState = string.Empty;
                                shipToCountry = string.Empty;

                                statusCounter = 0;
                            }
                        }
                    }
                }
            }

            return returnHead;
        }

        public List<SalesHeader> GetSalesOrders(string custName, string shipAddress)
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
            string warrantyType = string.Empty;
            string isPDA = string.Empty;
            string rmaNo = string.Empty;
            List<Comment> commentLines = new List<Comment>();

            bool rmaExists = false;
            bool isExchangeOrder = false;
            bool isPartRequest = false;
            string quoteOrderNo = string.Empty;
            bool allowRefund = false;

            string sellToCustomerNo = string.Empty;

            List<string> insertedOrderNumbers = new List<string>();
            string ebayUserID = string.Empty;

            List<PartialRefunded> partialRefunds = new List<PartialRefunded>();

            int statusCounter = 0;

            if (currResults.SOImportBuffer != null)
            {
                for (int so = 0; so < currResults.SOImportBuffer.Length; so++)
                {
                    if (currResults.SOImportBuffer[so].OrderType == "Sales Order")
                    {
                        if (currResults.SOImportBuffer[so].ShipToName.ToUpper() == custName && currResults.SOImportBuffer[so].ShipToAddress.ToUpper() == shipAddress)
                        {
                            orderNo = currResults.SOImportBuffer[so].SalesOrderNo;

                            if (!insertedOrderNumbers.Any(order => order.Equals(orderNo)))
                            {
                                orderStatus = currResults.SOImportBuffer[so].OrderStatus;
                                orderDate = currResults.SOImportBuffer[so].OrderDate;
                                channelName = currResults.SOImportBuffer[so].ChannelName[0];
                                externalDocumentNo = currResults.SOImportBuffer[so].ExternalDocumentNo;
                                shipHeader = ReturnShipmentHeader(orderNo);

                                foreach (ShipmentHeader sh in shipHeader)
                                {
                                    postPackage.AddRange(ReturnPostedPackage(orderNo, sh.No));
                                }

                                status = currResults.SOImportBuffer[so].Warranty[0].Status[0];
                                policy = currResults.SOImportBuffer[so].Warranty[0].Policy[0]; ;
                                daysRemaining = currResults.SOImportBuffer[so].Warranty[0].DaysRemaining[0];
                                warrantyType = currResults.SOImportBuffer[so].Warranty[0].WarrantyType[0];
                                isPDA = currResults.SOImportBuffer[so].Warranty[0].IsPDAWarranty[0].ToUpper();
                                allowRefund = currResults.SOImportBuffer[so].Warranty[0].RefundAllowed[0] == "Yes" ? true : false;
                                warranty = new Warranty(status, policy, int.Parse(daysRemaining), warrantyType, isPDA, allowRefund);
                                sellToCustomerNo = currResults.SOImportBuffer[so].CustomerNo;

                                // NJ - 11 January 2019 - Allow any customers channels of containing ebay condition
                                if (currResults.SOImportBuffer[so].CustomerNo.ToLower().Contains("ebay"))
                                {
                                    ebayUserID = currResults.SOImportBuffer[so].EbayUserID;
                                }

                                if (currResults.ExtendedSalesHeader != null)
                                {
                                    for (int ex = 0; ex < currResults.ExtendedSalesHeader.Length; ex++)
                                    {
                                        foreach (ShipmentHeader sh in shipHeader)
                                        {
                                            if (currResults.ExtendedSalesHeader[ex].SSHNo == sh.No)
                                            {
                                                rmaExists = true;
                                            }
                                            if (currResults.ExtendedSalesHeader[ex].RMANo == sh.No)
                                            {
                                                isExchangeOrder = currResults.ExtendedSalesHeader[ex].IsExchangeOrder == "Yes" ? true : false;
                                            }
                                        }
                                    }
                                }

                                //9 November 2018 - Updated logic to pull comments using SSH No aswell
                                commentLines.AddRange(GetSalesLineComments(orderNo));
                                foreach (ShipmentHeader sh in shipHeader)
                                {
                                    commentLines.AddRange(GetSalesLineComments(sh.No));
                                }

                                partialRefunds = GetPartialRefunds(orderNo, externalDocumentNo);
                                salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo,
                                    warranty, rmaExists, rmaNo, isExchangeOrder, sellToCustomerNo, commentLines, isPartRequest, quoteOrderNo,
                                    partialRefunds, ebayUserID));

                                insertedOrderNumbers.Add(orderNo);

                                orderStatus = string.Empty;
                                orderDate = string.Empty;
                                orderNo = string.Empty;
                                channelName = string.Empty;
                                shipHeader = new List<ShipmentHeader>();
                                postPackage = new List<PostedPackage>();
                                externalDocumentNo = string.Empty;
                                warranty = new Warranty();
                                commentLines = new List<Comment>();
                                partialRefunds = new List<PartialRefunded>();

                                status = string.Empty;
                                policy = string.Empty;
                                daysRemaining = string.Empty;
                                warrantyType = string.Empty;
                                isPDA = string.Empty;
                                rmaExists = false;
                                rmaNo = string.Empty;
                                isExchangeOrder = false;
                                isPartRequest = false;
                                sellToCustomerNo = string.Empty;
                                quoteOrderNo = string.Empty;
                                allowRefund = false;
                                ebayUserID = string.Empty;
                            }
                        }
                    }
                }
                //if (salesHead.Count > 0)
                //    return salesHead;
            }

            if (currResults.SalesHeader != null)
            {
                for (int so = 0; so < currResults.SalesHeader.Length; so++)
                {
                    if (currResults.SalesHeader[so].DocType == "Order" || currResults.SalesHeader[so].DocType == "Quote")
                    {
                        if (currResults.SalesHeader[so].ShipToName.ToUpper() == custName && currResults.SalesHeader[so].ShipToAddress.ToUpper() == shipAddress)
                        {
                            orderNo = currResults.SalesHeader[so].No;

                            if (!insertedOrderNumbers.Any(order => order.Equals(orderNo)))
                            {
                                orderDate = currResults.SalesHeader[so].DocDate;
                                channelName = currResults.SalesHeader[so].SellToCustomerName;
                                externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;
                                shipHeader = ReturnShipmentHeader(orderNo);
                                rmaNo = currResults.SalesHeader[so].RMANo;

                                foreach (ShipmentHeader sh in shipHeader)
                                {
                                    postPackage.AddRange(ReturnPostedPackage(orderNo, sh.No));
                                }

                                status = currResults.SalesHeader[so].Warranty2[0].Status2[0];
                                policy = currResults.SalesHeader[so].Warranty2[0].Policy2[0]; ;
                                daysRemaining = currResults.SalesHeader[so].Warranty2[0].DaysRemaining2[0];
                                warrantyType = currResults.SalesHeader[so].Warranty2[0].WarrantyType2[0];
                                isPDA = currResults.SalesHeader[so].Warranty2[0].IsPDAWarranty2[0].ToUpper();
                                allowRefund = currResults.SalesHeader[so].Warranty2[0].RefundAllowed2[0] == "Yes" ? true : false;
                                warranty = new Warranty(status, policy, int.Parse(daysRemaining), warrantyType, isPDA, allowRefund);
                                sellToCustomerNo = currResults.SalesHeader[so].SellToCustomerNo;
                                int totalCounter = 0;

                                for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                {
                                    if ((currResults.SalesLine[sl].DocNo == orderNo) && (currResults.SalesLine[sl].Type == "Item"))
                                    {
                                        totalCounter++;
                                        int.TryParse(currResults.SalesLine[sl].QtyShipped, out int qtyShipped);
                                        if (qtyShipped > 0)
                                        {
                                            statusCounter++;
                                        }
                                    }
                                }

                                if (statusCounter == totalCounter)
                                {
                                    orderStatus = "Shipped";
                                }
                                else if (statusCounter == 0)
                                {
                                    orderStatus = "OrderCreated";
                                }
                                else
                                {
                                    orderStatus = "Partial Shipped";
                                }


                                if (currResults.ExtendedSalesHeader != null)
                                {
                                    for (int ex = 0; ex < currResults.ExtendedSalesHeader.Length; ex++)
                                    {
                                        foreach (ShipmentHeader sh in shipHeader)
                                        {
                                            if (currResults.ExtendedSalesHeader[ex].SSHNo == sh.No)
                                            {
                                                rmaExists = true;
                                            }
                                            if (currResults.ExtendedSalesHeader[ex].RMANo == sh.No)
                                            {
                                                isExchangeOrder = currResults.ExtendedSalesHeader[ex].IsExchangeOrder == "Yes" ? true : false;
                                            }
                                        }

                                        if (orderNo == currResults.ExtendedSalesHeader[ex].RMANo && currResults.ExtendedSalesHeader[ex].IsPartRequest == "Yes")
                                        {
                                            isPartRequest = true;
                                            quoteOrderNo = currResults.ExtendedSalesHeader[ex].PartRequestOrderNo;
                                        }
                                    }
                                }

                                //9 November 2018 - Updated logic to pull comments using SSH No aswell
                                commentLines.AddRange(GetSalesLineComments(orderNo));
                                foreach (ShipmentHeader sh in shipHeader)
                                {
                                    commentLines.AddRange(GetSalesLineComments(sh.No));
                                }

                                partialRefunds = GetPartialRefunds(orderNo, externalDocumentNo);
                                salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty,
                                    rmaExists, rmaNo, isExchangeOrder, sellToCustomerNo, commentLines, isPartRequest, quoteOrderNo,
                                    partialRefunds, ebayUserID));

                                insertedOrderNumbers.Add(orderNo);

                                orderStatus = string.Empty;
                                orderDate = string.Empty;
                                orderNo = string.Empty;
                                channelName = string.Empty;
                                shipHeader = new List<ShipmentHeader>();
                                postPackage = new List<PostedPackage>();
                                externalDocumentNo = string.Empty;
                                warranty = new Warranty();
                                commentLines = new List<Comment>();
                                partialRefunds = new List<PartialRefunded>();

                                status = string.Empty;
                                policy = string.Empty;
                                daysRemaining = string.Empty;
                                warrantyType = string.Empty;
                                isPDA = string.Empty;
                                rmaExists = false;
                                rmaNo = string.Empty;
                                isExchangeOrder = false;
                                isPartRequest = false;
                                sellToCustomerNo = string.Empty;
                                quoteOrderNo = string.Empty;
                                allowRefund = false;
                                ebayUserID = string.Empty;
                            }
                        }
                    }
                }
                // if (salesHead.Count > 0)
                //   return salesHead;
            }

            if (currResults.SalesShipmentHeader != null)
            {
                for (int so = 0; so < currResults.SalesShipmentHeader.Length; so++)
                {
                    if (currResults.SalesShipmentHeader[so].ShipToName.ToUpper() == custName && currResults.SalesShipmentHeader[so].ShipToAddress.ToUpper() == shipAddress)
                    {
                        orderNo = currResults.SalesShipmentHeader[so].OrderNo;

                        if (!insertedOrderNumbers.Any(order => order.Equals(orderNo)))
                        {
                            orderDate = currResults.SalesShipmentHeader[so].OrderDate;
                            channelName = currResults.SalesShipmentHeader[so].SellToCustomerName;
                            externalDocumentNo = currResults.SalesShipmentHeader[so].ExtDocNo;
                            shipHeader = ReturnShipmentHeader(orderNo);

                            foreach (ShipmentHeader sh in shipHeader)
                            {
                                postPackage.AddRange(ReturnPostedPackage(orderNo, sh.No));
                            }

                            status = currResults.SalesShipmentHeader[so].Warranty3[0].Status3[0];
                            policy = currResults.SalesShipmentHeader[so].Warranty3[0].Policy3[0]; ;
                            daysRemaining = currResults.SalesShipmentHeader[so].Warranty3[0].DaysRemaining3[0];
                            warrantyType = currResults.SalesShipmentHeader[so].Warranty3[0].WarrantyType3[0];
                            isPDA = currResults.SalesShipmentHeader[so].Warranty3[0].IsPDAWarranty3[0].ToUpper();
                            allowRefund = currResults.SalesShipmentHeader[so].Warranty3[0].RefundAllowed3[0] == "Yes" ? true : false;
                            warranty = new Warranty(status, policy, int.Parse(daysRemaining), warrantyType, isPDA, allowRefund);
                            sellToCustomerNo = currResults.SalesShipmentHeader[so].SellToCustomerNo;

                            orderStatus = "Shipped";


                            if (currResults.ExtendedSalesHeader != null)
                            {
                                for (int ex = 0; ex < currResults.ExtendedSalesHeader.Length; ex++)
                                {
                                    foreach (ShipmentHeader sh in shipHeader)
                                    {
                                        if (currResults.ExtendedSalesHeader[ex].SSHNo == sh.No)
                                        {
                                            rmaExists = true;
                                        }
                                        if (currResults.ExtendedSalesHeader[ex].RMANo == sh.No)
                                        {
                                            isExchangeOrder = currResults.ExtendedSalesHeader[ex].IsExchangeOrder == "Yes" ? true : false;
                                        }
                                    }
                                }
                            }

                            // 2 November 2018 -  Updated to use SSH No.
                            commentLines = GetSalesLineComments(currResults.SalesShipmentHeader[so].No);
                            partialRefunds = GetPartialRefunds(orderNo, externalDocumentNo);
                            salesHead.Add(new SalesHeader(orderStatus, orderDate, orderNo, channelName, shipHeader, postPackage, externalDocumentNo, warranty,
                                rmaExists, rmaNo, isExchangeOrder, sellToCustomerNo, commentLines, isPartRequest, quoteOrderNo,
                                partialRefunds, ebayUserID));

                            insertedOrderNumbers.Add(orderNo);

                            orderStatus = string.Empty;
                            orderDate = string.Empty;
                            orderNo = string.Empty;
                            channelName = string.Empty;
                            shipHeader = new List<ShipmentHeader>();
                            postPackage = new List<PostedPackage>();
                            externalDocumentNo = string.Empty;
                            warranty = new Warranty();
                            commentLines = new List<Comment>();
                            partialRefunds = new List<PartialRefunded>();

                            status = string.Empty;
                            policy = string.Empty;
                            daysRemaining = string.Empty;
                            warrantyType = string.Empty;
                            isPDA = string.Empty;
                            rmaExists = false;
                            rmaNo = string.Empty;
                            isExchangeOrder = false;
                            isPartRequest = false;
                            sellToCustomerNo = string.Empty;
                            quoteOrderNo = string.Empty;
                            allowRefund = false;
                            ebayUserID = string.Empty;
                        }
                    }
                }

                //if (salesHead.Count > 0)
                //    return salesHead;
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
            List<string> shipAddresses = new List<string>();

            List<string> readRMA = new List<string>();

            if (currResults.SOImportBuffer != null)
            {
                for (int c = 0; c < currResults.SOImportBuffer.Length; c++)
                {
                    shipToName = currResults.SOImportBuffer[c].ShipToName.ToUpper();
                    shipToAddress1 = currResults.SOImportBuffer[c].ShipToAddress.ToUpper();

                    if (!customerNames.Any(order => order.Equals(shipToName)) ||
                        !shipAddresses.Any(address => address.Equals(shipToAddress1)))
                    {
                        shipToAddress2 = currResults.SOImportBuffer[c].ShipToAddress2;
                        shipToContact = currResults.SOImportBuffer[c].ShipToContact;
                        shipToCity = currResults.SOImportBuffer[c].ShipToCity;
                        shipToZip = currResults.SOImportBuffer[c].ShipToZip;
                        shipToState = currResults.SOImportBuffer[c].ShipToState;
                        shipToCountry = currResults.SOImportBuffer[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName, shipToAddress1);

                        returnHeaders = GetReturnOrdersFromSalesHeader(salesHeaders, ref readRMA);
                        returnHeaders.AddRange(GetReturnOrdersFromShipmentHeader(salesHeaders, ref readRMA));

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);
                        shipAddresses.Add(shipToAddress1);

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
                    shipToName = currResults.SalesHeader[c].ShipToName.ToUpper();
                    shipToAddress1 = currResults.SalesHeader[c].ShipToAddress.ToUpper();

                    if (currResults.SalesHeader[c].DocType == "Order")
                    {
                        if (!customerNames.Any(order => order.Equals(shipToName)) ||
                            !shipAddresses.Any(address => address.Equals(shipToAddress1)))
                        {
                            shipToAddress2 = currResults.SalesHeader[c].ShipToAddress2;
                            shipToContact = currResults.SalesHeader[c].ShipToContact;
                            shipToCity = currResults.SalesHeader[c].ShipToCity;
                            shipToZip = currResults.SalesHeader[c].ShipToZip;
                            shipToState = currResults.SalesHeader[c].ShipToState;
                            shipToCountry = currResults.SalesHeader[c].ShipToCountry;
                            salesHeaders = GetSalesOrders(shipToName, shipToAddress1);

                            returnHeaders.AddRange(GetReturnOrders(shipToName, shipToAddress1, ref readRMA));
                            returnHeaders.AddRange(GetReturnOrdersFromSalesHeader(salesHeaders, ref readRMA));

                            returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                            customerNames.Add(shipToName);
                            shipAddresses.Add(shipToAddress1);

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
            }

            if (currResults.SalesShipmentHeader != null)
            {
                for (int c = 0; c < currResults.SalesShipmentHeader.Length; c++)
                {
                    shipToName = currResults.SalesShipmentHeader[c].ShipToName.ToUpper();
                    shipToAddress1 = currResults.SalesShipmentHeader[c].ShipToAddress.ToUpper();

                    if (!customerNames.Any(order => order.Equals(shipToName)) ||
                        !shipAddresses.Any(address => address.Equals(shipToAddress1)))
                    {
                        shipToAddress2 = currResults.SalesShipmentHeader[c].ShipToAddress2;
                        shipToContact = currResults.SalesShipmentHeader[c].ShipToContact;
                        shipToCity = currResults.SalesShipmentHeader[c].ShipToCity;
                        shipToZip = currResults.SalesShipmentHeader[c].ShipToZip;
                        shipToState = currResults.SalesShipmentHeader[c].ShipToState;
                        shipToCountry = currResults.SalesShipmentHeader[c].ShipToCountry;
                        salesHeaders = GetSalesOrders(shipToName, shipToAddress1);
                        returnHeaders.AddRange(GetReturnOrdersFromSalesHeader(salesHeaders, ref readRMA));
                        returnHeaders.AddRange(GetReturnOrdersFromShipmentHeader(salesHeaders, ref readRMA));

                        returnCust.Add(new Customer(shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToZip, shipToState, shipToCountry, salesHeaders, returnHeaders));
                        customerNames.Add(shipToName);
                        shipAddresses.Add(shipToAddress1);

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

            SetFunctionData();

            try
            {
                GetZendeskTickets(ref returnCust);
            }
            catch (Exception zendeskException)
            {
                HttpContext.Current.Session["ZendeskException"] = zendeskException.Message; 
            }

            return returnCust;
        }

        public List<ReturnHeader> GetReturnOrdersFromSalesHeader(List<SalesHeader> salesHeaders, ref List<string> readRMA)
        {
            /* 16 October 2018 - Neil Jansen
             * Updated logic to not match on external document no.s, but to loop through the extended sales header as we have updated the logic to link Sales Orders and Return Orders through this record.
             */

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
            string email = string.Empty;
            bool returnLabelCreated = false;
            bool exchangeCreated = false;
            List<string> exchangeOrderNo = new List<string>();
            string sellToCustomerNo = string.Empty;
            List<Comment> commentLines = new List<Comment>();
            string imeiNo = string.Empty;

            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToCode = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;

            List<string> insertedReturnNumbners = new List<string>();

            int statusCounter = 0;
            int totalCounter = 0;

            foreach (SalesHeader sh in salesHeaders)
            {
                if (currResults.SalesHeader != null)
                {
                    for (int so = 0; so < currResults.SalesHeader.Length; so++)
                    {
                        if (currResults.SalesHeader[so].DocType == "Return Order")
                        {
                            if (currResults.ExtendedSalesHeader != null)
                            {
                                for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                {
                                    if (currResults.ExtendedSalesHeader[esh].RMANo == currResults.SalesHeader[so].No)
                                    {
                                        foreach (ShipmentHeader ssh in sh.ShipmentHeaderObject)
                                        {
                                            if (currResults.ExtendedSalesHeader[esh].SSHNo == ssh.No)
                                            {
                                                rmaNo = currResults.SalesHeader[so].No;

                                                if (!readRMA.Any(rma => rma.Equals(rmaNo)))
                                                {
                                                    if (!insertedReturnNumbners.Any(order => order.Equals(rmaNo)))
                                                    {
                                                        receiptHeader = ReturnReceiptHeader(rmaNo);
                                                        channelName = currResults.SalesHeader[so].SellToCustomerName;
                                                        orderDate = currResults.SalesHeader[so].DocDate;
                                                        externalDocumentNo = currResults.SalesHeader[so].ExtDocNo;
                                                        returnTrackingNo = currResults.SalesHeader[so].ReturnTrackingNo;

                                                        if (receiptHeader.Count == 0)
                                                        {
                                                            receiptHeader.AddRange(ReturnShipmentReceiptHeader(sh.SalesOrderNo, externalDocumentNo, rmaNo));
                                                        }
                                                        else
                                                        {
                                                            foreach (ReceiptHeader rh in receiptHeader)
                                                            {
                                                                postedReceive.AddRange(ReturnPostedReceive(rmaNo, rh.No));
                                                            }
                                                        }

                                                        returnLabelCreated = currResults.SalesHeader[so].UPSRetLabelCreated.ToUpper() == "YES" ? true : false;
                                                        sellToCustomerNo = currResults.SalesHeader[so].SellToCustomerNo;
                                                        imeiNo = currResults.SalesHeader[so].IMEI;

                                                        if (currResults.SalesHeader[so].RMANo != "")
                                                        {
                                                            exchangeCreated = true;

                                                            if (currResults.SalesHeader[so].ExchangeOrderNos[0].Contains("|"))
                                                            {
                                                                string[] tempSplit = currResults.SalesHeader[so].ExchangeOrderNos[0].Split('|');

                                                                for (int i = 0; i < tempSplit.Length; i++)
                                                                {
                                                                    exchangeOrderNo.Add(tempSplit[i]);
                                                                }
                                                            }
                                                            else
                                                            {
                                                                exchangeOrderNo.Add(currResults.SalesHeader[so].ExchangeOrderNos[0]);
                                                            }
                                                        }

                                                        if (currResults.SalesLine != null)
                                                        {
                                                            for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                                            {
                                                                if ((currResults.SalesLine[sl].DocNo == rmaNo) && (currResults.SalesLine[sl].Type == "Item"))
                                                                {
                                                                    totalCounter++;
                                                                    dateCreated = currResults.SalesLine[sl].DateCreated;
                                                                    int.TryParse(currResults.SalesLine[sl].QtyToReceive, out int qtyToRec);
                                                                    if (qtyToRec > 0)
                                                                    {
                                                                        statusCounter++;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (statusCounter == totalCounter)
                                                        {
                                                            returnStatus = "Open";
                                                        }
                                                        else if (statusCounter == 0)
                                                        {
                                                            returnStatus = "Received";
                                                        }
                                                        else
                                                        {
                                                            returnStatus = "Partial Receipt";
                                                        }

                                                        if (currResults.ExtendedSalesHeader != null)
                                                        {
                                                            for (int hse = 0; hse < currResults.ExtendedSalesHeader.Length; hse++)
                                                            {
                                                                if (currResults.ExtendedSalesHeader[hse].RMANo == rmaNo)
                                                                {
                                                                    email = currResults.ExtendedSalesHeader[hse].Email;

                                                                    if (currResults.ExtendedSalesHeader[hse].IsRefund.ToUpper() == "YES")
                                                                    {
                                                                        returnStatus = currResults.ExtendedSalesHeader[hse].RefundStatus;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        shipToName = currResults.SalesHeader[so].ShipToName;
                                                        shipToAddress1 = currResults.SalesHeader[so].ShipToAddress;
                                                        shipToAddress2 = currResults.SalesHeader[so].ShipToAddress2;
                                                        shipToContact = currResults.SalesHeader[so].ShipToContact;
                                                        shipToCity = currResults.SalesHeader[so].ShipToCity;
                                                        shipToCode = currResults.SalesHeader[so].ShipToZip;
                                                        shipToState = currResults.SalesHeader[so].ShipToState;
                                                        shipToCountry = currResults.SalesHeader[so].ShipToCountry;

                                                        //9 November 2018 - Updated logic to pull comments using Receipt No aswell
                                                        commentLines.AddRange(GetSalesLineComments(rmaNo));
                                                        foreach (ReceiptHeader rh in receiptHeader)
                                                        {
                                                            commentLines.AddRange(GetSalesLineComments(rh.No));
                                                        }
                                                        returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate,
                                                            rmaNo, externalDocumentNo, email, returnLabelCreated, exchangeCreated, exchangeOrderNo, sellToCustomerNo, commentLines, imeiNo,
                                                        shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToCode, shipToState, shipToCountry));
                                                        insertedReturnNumbners.Add(rmaNo);
                                                        readRMA.Add(rmaNo);

                                                        returnStatus = string.Empty;
                                                        dateCreated = string.Empty;
                                                        channelName = string.Empty;
                                                        receiptHeader = new List<ReceiptHeader>();
                                                        postedReceive = new List<PostedReceive>();
                                                        returnTrackingNo = string.Empty;
                                                        orderDate = string.Empty;
                                                        rmaNo = string.Empty;
                                                        externalDocumentNo = string.Empty;
                                                        email = string.Empty;
                                                        returnLabelCreated = false;
                                                        exchangeCreated = false;
                                                        exchangeOrderNo = new List<string>();
                                                        commentLines = new List<Comment>();
                                                        imeiNo = string.Empty;

                                                        shipToName = string.Empty;
                                                        shipToAddress1 = string.Empty;
                                                        shipToAddress2 = string.Empty;
                                                        shipToContact = string.Empty;
                                                        shipToCity = string.Empty;
                                                        shipToCode = string.Empty;
                                                        shipToState = string.Empty;
                                                        shipToCountry = string.Empty;

                                                        totalCounter = 0;
                                                        statusCounter = 0;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return returnHead;
        }

        public List<ReturnHeader> GetReturnOrdersFromShipmentHeader(List<SalesHeader> salesHeaders, ref List<string> readRMA)
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
            string email = string.Empty;
            string sellToCustomerNo = string.Empty;
            List<Comment> commentLines = new List<Comment>();
            string imeiNo = string.Empty;

            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToCode = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;

            List<string> insertedReturnNumbners = new List<string>();

            foreach (SalesHeader sh in salesHeaders)
            {
                foreach (ShipmentHeader ssh in sh.ShipmentHeaderObject)
                {
                    if (currResults.ReturnReceiptHeader != null)
                    {
                        for (int so = 0; so < currResults.ReturnReceiptHeader.Length; so++)
                        {
                            if (currResults.ReturnReceiptHeader[so].ExtDocNo == ssh.ExternalDocumentNo &&
                                currResults.ReturnReceiptHeader[so].SellToCustomerNo == ssh.SellToCustomerNo)
                            {
                                rmaNo = currResults.ReturnReceiptHeader[so].ReturnOrderNo;

                                if (!readRMA.Any(rma => rma.Equals(rmaNo)))
                                {
                                    if (!insertedReturnNumbners.Any(order => order.Equals(rmaNo)))
                                    {
                                        dateCreated = currResults.ReturnReceiptHeader[so].ReceiveDate;
                                        receiptHeader = ReturnReceiptHeader(rmaNo);
                                        channelName = currResults.ReturnReceiptHeader[so].SellToCustomerName;
                                        orderDate = currResults.ReturnReceiptHeader[so].OrderDate;
                                        externalDocumentNo = currResults.ReturnReceiptHeader[so].ExtDocNo;
                                        sellToCustomerNo = currResults.ReturnReceiptHeader[so].SellToCustomerNo;

                                        if (receiptHeader.Count == 0)
                                        {
                                            receiptHeader.AddRange(ReturnShipmentReceiptHeader(sh.SalesOrderNo, externalDocumentNo, rmaNo));
                                        }
                                        else
                                        {
                                            foreach (ReceiptHeader rh in receiptHeader)
                                            {
                                                postedReceive.AddRange(ReturnPostedReceive(rmaNo, rh.No));
                                            }
                                        }

                                        returnStatus = "Received";

                                        if (currResults.ExtendedSalesHeader != null)
                                        {
                                            for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                                            {
                                                if (currResults.ExtendedSalesHeader[esh].RMANo == rmaNo)
                                                {
                                                    email = currResults.ExtendedSalesHeader[esh].Email;

                                                    if (currResults.ExtendedSalesHeader[esh].IsRefund.ToUpper() == "YES")
                                                    {
                                                        returnStatus = currResults.ExtendedSalesHeader[esh].RefundStatus;
                                                    }
                                                }
                                            }
                                        }

                                        shipToName = currResults.ReturnReceiptHeader[so].ShipToName;
                                        shipToAddress1 = currResults.ReturnReceiptHeader[so].ShipToAddress;
                                        shipToAddress2 = currResults.ReturnReceiptHeader[so].ShipToAddress2;
                                        shipToContact = currResults.ReturnReceiptHeader[so].ShipToContact;
                                        shipToCity = currResults.ReturnReceiptHeader[so].ShipToCity;
                                        shipToCode = currResults.ReturnReceiptHeader[so].ShipToZip;
                                        shipToState = currResults.ReturnReceiptHeader[so].ShipToState;
                                        shipToCountry = currResults.ReturnReceiptHeader[so].ShipToCountry;

                                        //9 November 2018 - Updated logic to pull comments using Receipt No aswell
                                        commentLines.AddRange(GetSalesLineComments(rmaNo));
                                        foreach (ReceiptHeader rh in receiptHeader)
                                        {
                                            commentLines.AddRange(GetSalesLineComments(rh.No));
                                        }
                                        returnHead.Add(new ReturnHeader(returnStatus, dateCreated, channelName, receiptHeader, postedReceive, returnTrackingNo, orderDate,
                                            rmaNo, externalDocumentNo, email, false, false, new List<string>(), sellToCustomerNo, commentLines, imeiNo,
                                        shipToName, shipToAddress1, shipToAddress2, shipToContact, shipToCity, shipToCode, shipToState, shipToCountry));
                                        insertedReturnNumbners.Add(rmaNo);
                                        readRMA.Add(rmaNo);

                                        returnStatus = string.Empty;
                                        dateCreated = string.Empty;
                                        channelName = string.Empty;
                                        receiptHeader = new List<ReceiptHeader>();
                                        postedReceive = new List<PostedReceive>();
                                        returnTrackingNo = string.Empty;
                                        orderDate = string.Empty;
                                        rmaNo = string.Empty;
                                        externalDocumentNo = string.Empty;
                                        email = string.Empty;
                                        commentLines = new List<Comment>();
                                        imeiNo = string.Empty;

                                        shipToName = string.Empty;
                                        shipToAddress1 = string.Empty;
                                        shipToAddress2 = string.Empty;
                                        shipToContact = string.Empty;
                                        shipToCity = string.Empty;
                                        shipToCode = string.Empty;
                                        shipToState = string.Empty;
                                        shipToCountry = string.Empty;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return returnHead;
        }

        private List<ReceiptHeader> ReturnShipmentReceiptHeader(string orderNo, string extDocNo, string rmaNo)
        {
            //Before Extended Sales Header linking shipments to returns
            List<ReceiptHeader> receipts = new List<ReceiptHeader>();

            string no = string.Empty;
            string externalDocumentNo = string.Empty;
            string receiptDate = string.Empty;
            List<ReceiptLine> receiptLines = new List<ReceiptLine>();
            string shippingAgentCode = string.Empty;
            string shippingAgentService = string.Empty;

            //Check extended sales header
            string extendedSalesHeaderSSHNo = string.Empty;

            if (currResults.ExtendedSalesHeader != null)
            {
                for (int ex = 0; ex < currResults.ExtendedSalesHeader.Length; ex++)
                {
                    if (currResults.ExtendedSalesHeader[ex].RMANo == rmaNo)
                    {
                        extendedSalesHeaderSSHNo = currResults.ExtendedSalesHeader[ex].SSHNo;

                        for (int exs = 0; exs < currResults.SalesShipmentHeader.Length; exs++)
                        {
                            if (extendedSalesHeaderSSHNo == currResults.SalesShipmentHeader[exs].No)
                            {
                                no = rmaNo;
                                externalDocumentNo = extDocNo;
                                receiptLines = ReturnSalesLineReceiptLines(rmaNo);

                                receipts.Add(new ReceiptHeader(no, externalDocumentNo, receiptDate, receiptLines, shippingAgentCode, true));

                                no = string.Empty;
                                externalDocumentNo = string.Empty;
                                receiptDate = string.Empty;
                                receiptLines = new List<ReceiptLine>();
                                shippingAgentCode = string.Empty;
                                shippingAgentService = string.Empty;
                            }
                        }
                    }
                }
            }

            if (receipts.Count > 0)
            {
                return receipts;
            }
            //

            if (currResults.SalesShipmentHeader != null)
            {
                for (int sh = 0; sh < currResults.SalesShipmentHeader.Length; sh++)
                {
                    if (currResults.SalesShipmentHeader[sh].ExtDocNo == extDocNo &&
                        currResults.SalesShipmentHeader[sh].OrderNo == orderNo)
                    {
                        externalDocumentNo = extDocNo;
                        receiptLines = ReturnShipmentReceiptLines(currResults.SalesShipmentHeader[sh].No);

                        receipts.Add(new ReceiptHeader(no, externalDocumentNo, receiptDate, receiptLines, shippingAgentCode, true));

                        no = string.Empty;
                        externalDocumentNo = string.Empty;
                        receiptDate = string.Empty;
                        receiptLines = new List<ReceiptLine>();
                        shippingAgentCode = string.Empty;
                        shippingAgentService = string.Empty;
                    }
                }
            }

            return receipts;
        }

        private List<ReceiptLine> ReturnShipmentReceiptLines(string no)
        {
            List<ReceiptLine> receiptLines = new List<ReceiptLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityReceived = 0;
            double price = 0;
            double lineAmount = 0;
            int quantityExchanged = 0;
            int quantityRefunded = 0;
            string reqReturnAction = string.Empty;
            string returnReason = string.Empty;
            string crossRefNo = string.Empty;

            if (currResults.SalesShipmentLine != null)
            {
                for (int rl = 0; rl < currResults.SalesShipmentLine.Length; rl++)
                {
                    if (currResults.SalesShipmentLine[rl].DocNo == no)
                    {
                        itemNo = currResults.SalesShipmentLine[rl].ItemNo;
                        description = currResults.SalesShipmentLine[rl].Description;
                        int.TryParse(currResults.SalesShipmentLine[rl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                        double.TryParse(currResults.SalesShipmentLine[rl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;
                        quantityReceived = 0;
                        quantityExchanged = 0;
                        quantityRefunded = 0;
                        crossRefNo = currResults.SalesShipmentLine[rl].CrossRefNo;

                        receiptLines.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount, quantityExchanged, quantityRefunded, string.Empty, string.Empty, crossRefNo));
                    }
                }
            }

            return receiptLines;
        }

        private List<ReceiptLine> ReturnSalesLineReceiptLines(string no)
        {
            List<ReceiptLine> receiptLines = new List<ReceiptLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityReceived = 0;
            double price = 0;
            double lineAmount = 0;
            int quantityExchanged = 0;
            int quantityRefunded = 0;
            string reqReturnAction = string.Empty;
            string returnReason = string.Empty;
            string crossRefNo = string.Empty;

            if (currResults.SalesLine != null)
            {
                for (int slr = 0; slr < currResults.SalesLine.Length; slr++)
                {
                    if (currResults.SalesLine[slr].DocNo == no)
                    {
                        itemNo = currResults.SalesLine[slr].ItemNo;
                        description = currResults.SalesLine[slr].Description;
                        int.TryParse(currResults.SalesLine[slr].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                        double.TryParse(currResults.SalesLine[slr].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;
                        quantityReceived = 0;
                        quantityExchanged = 0;
                        quantityRefunded = 0;

                        reqReturnAction = currResults.SalesLine[slr].REQReturnAction;
                        returnReason = currResults.SalesLine[slr].ReturnReason;
                        crossRefNo = currResults.SalesLine[slr].CrossRefNo;

                        receiptLines.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount, quantityExchanged, quantityRefunded, reqReturnAction, returnReason, crossRefNo));

                        itemNo = string.Empty;
                        description = string.Empty;
                        quantity = 0;
                        quantityReceived = 0;
                        price = 0;
                        lineAmount = 0;
                        quantityExchanged = 0;
                        quantityRefunded = 0;
                        reqReturnAction = string.Empty;
                        returnReason = string.Empty;
                        crossRefNo = string.Empty;
                    }
                }
            }

            return receiptLines;
        }

        private List<ReceiptLine> GetShipmentReturnLines(string no)
        {
            List<ReceiptLine> singleReceipt = new List<ReceiptLine>();
            List<ReceiptLine> receiptLines = new List<ReceiptLine>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            int quantityReceived = 0;
            double price = 0;
            double lineAmount = 0;
            List<string> insertedItems = new List<string>();
            int quantityExchanged = 0;
            int quantityRefunded = 0;
            string reqReturnAction = string.Empty;
            string returnReason = string.Empty;
            string crossRefNo = string.Empty;

            List<string> rmaNo = new List<string>();

            if (currResults.ExtendedSalesHeader != null)
            {
                for (int esh = 0; esh < currResults.ExtendedSalesHeader.Length; esh++)
                {
                    if (currResults.ExtendedSalesHeader[esh].SSHNo == no)
                    {
                        rmaNo.Add(currResults.ExtendedSalesHeader[esh].RMANo);
                    }
                }
            }

            if (rmaNo.Count != 0)
            {
                if (currResults.SalesLine != null)
                {
                    for (int slr = 0; slr < currResults.SalesLine.Length; slr++)
                    {
                        foreach (string rmaLine in rmaNo)
                        {
                            if (currResults.SalesLine[slr].DocNo == rmaLine)
                            {
                                itemNo = currResults.SalesLine[slr].ItemNo;
                                description = currResults.SalesLine[slr].Description;
                                int.TryParse(currResults.SalesLine[slr].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                                double.TryParse(currResults.SalesLine[slr].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                                lineAmount = quantity * price;
                                quantityReceived = 0;
                                quantityExchanged = 0;
                                quantityRefunded = 0;

                                crossRefNo = currResults.SalesLine[slr].CrossRefNo;

                                if (insertedItems.Any(item => item.Equals(itemNo)))
                                {
                                    foreach (ReceiptLine existingItem in receiptLines)
                                    {
                                        if (existingItem.ItemNo == itemNo)
                                        {
                                            existingItem.Quantity += quantity;
                                            existingItem.LineAmount = existingItem.Quantity * price;
                                        }
                                    }
                                }
                                else
                                {
                                    for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                    {
                                        if ((currResults.SalesLine[sl].DocNo == rmaLine) && (currResults.SalesLine[sl].ItemNo == itemNo))
                                        {
                                            int.TryParse(currResults.SalesLine[sl].QtyExchanged, out quantityExchanged);
                                            int.TryParse(currResults.SalesLine[sl].QtyRefunded, out quantityRefunded);
                                            reqReturnAction = currResults.SalesLine[sl].REQReturnAction;
                                            returnReason = currResults.SalesLine[sl].ReturnReason;
                                        }
                                    }
                                }

                                receiptLines.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount, quantityExchanged, quantityRefunded, reqReturnAction, returnReason, crossRefNo));
                                insertedItems.Add(itemNo);

                                itemNo = string.Empty;
                                description = string.Empty;
                                quantity = 0;
                                quantityReceived = 0;
                                price = 0;
                                lineAmount = 0;
                                quantityExchanged = 0;
                                quantityRefunded = 0;
                                reqReturnAction = string.Empty;
                                returnReason = string.Empty;
                                crossRefNo = string.Empty;
                            }
                        }
                    }
                }

                if (currResults.ReturnReceiptHeader != null)
                {
                    for (int rrh = 0; rrh < currResults.ReturnReceiptHeader.Length; rrh++)
                    {
                        if (currResults.ReturnReceiptLine != null)
                        {
                            for (int rl = 0; rl < currResults.ReturnReceiptLine.Length; rl++)
                            {
                                if (currResults.ReturnReceiptHeader[rrh].No == currResults.ReturnReceiptLine[rl].DocNo)
                                {
                                    foreach (string rmaLine in rmaNo)
                                    {
                                        if (currResults.ReturnReceiptHeader[rrh].ReturnOrderNo == rmaLine)
                                        {
                                            itemNo = currResults.ReturnReceiptLine[rl].ItemNo;
                                            description = currResults.ReturnReceiptLine[rl].Description;
                                            int.TryParse(currResults.ReturnReceiptLine[rl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out quantity);
                                            double.TryParse(currResults.ReturnReceiptLine[rl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                                            lineAmount = quantity * price;
                                            crossRefNo = currResults.ReturnReceiptLine[rl].CrossRefNo;

                                            if (insertedItems.Any(item => item.Equals(itemNo)))
                                            {
                                                foreach (ReceiptLine existingItem in receiptLines)
                                                {
                                                    if (existingItem.ItemNo == itemNo)
                                                    {
                                                        existingItem.Quantity += quantity;
                                                        existingItem.LineAmount = existingItem.Quantity * price;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                for (int rli = 0; rli < currResults.ReturnReceiptLine.Length; rli++)
                                                {
                                                    if ((currResults.ReturnReceiptLine[rli].DocNo == no) && (currResults.ReturnReceiptLine[rli].ItemNo == itemNo))
                                                    {
                                                        int.TryParse(currResults.ReturnReceiptLine[rli].Qty, out int currQty);
                                                        quantityReceived += currQty;
                                                    }
                                                }

                                                if (currResults.SalesLine != null)
                                                {
                                                    for (int sl = 0; sl < currResults.SalesLine.Length; sl++)
                                                    {
                                                        if ((currResults.SalesLine[sl].DocNo == rmaLine) && (currResults.SalesLine[sl].ItemNo == itemNo))
                                                        {
                                                            int.TryParse(currResults.SalesLine[sl].QtyExchanged, out quantityExchanged);
                                                            int.TryParse(currResults.SalesLine[sl].QtyRefunded, out quantityRefunded);
                                                            reqReturnAction = currResults.SalesLine[sl].REQReturnAction;
                                                            returnReason = currResults.SalesLine[sl].ReturnReason;
                                                            crossRefNo = currResults.SalesLine[sl].CrossRefNo;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (currResults.SalesCreditMemo != null)
                                                    {
                                                        for (int scm = 0; scm < currResults.SalesCreditMemo.Length; scm++)
                                                        {
                                                            if (currResults.SalesCreditMemo[scm].ReturnOrderNo == rmaLine)
                                                            {
                                                                if (currResults.SalesCreditMemoLines != null)
                                                                {
                                                                    returnReason = currResults.ReturnReceiptLine[rl].ReturnReasonCode;
                                                                    reqReturnAction = currResults.ReturnReceiptLine[rl].REQReturnAction;

                                                                    for (int scml = 0; scml < currResults.SalesCreditMemoLines.Length; scml++)
                                                                    {
                                                                        if ((currResults.SalesCreditMemoLines[scml].DocNo == currResults.SalesCreditMemo[scm].No) && (currResults.SalesCreditMemoLines[scml].ItemNo == itemNo))
                                                                        {
                                                                            quantityExchanged = (int)Convert.ToDouble(currResults.SalesCreditMemoLines[scml].QtyExchanged, CultureInfo.InvariantCulture.NumberFormat);
                                                                            quantityRefunded = (int)Convert.ToDouble(currResults.SalesCreditMemoLines[scml].QtyRefunded, CultureInfo.InvariantCulture.NumberFormat);
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }

                                                receiptLines.Add(new ReceiptLine(itemNo, description, quantity, quantityReceived, price, lineAmount, quantityExchanged, quantityRefunded, reqReturnAction, returnReason, crossRefNo));
                                                insertedItems.Add(itemNo);
                                            }

                                            itemNo = string.Empty;
                                            description = string.Empty;
                                            quantity = 0;
                                            quantityReceived = 0;
                                            price = 0;
                                            lineAmount = 0;
                                            quantityExchanged = 0;
                                            quantityRefunded = 0;
                                            reqReturnAction = string.Empty;
                                            returnReason = string.Empty;
                                            crossRefNo = string.Empty;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return receiptLines;
        }

        private List<Comment> GetSalesLineComments(string docNo)
        {
            List<Comment> lineComments = new List<Comment>();
            string date = string.Empty;
            string comment = string.Empty;

            if (currResults.SalesCommentLine != null)
            {
                for (int scl = 0; scl < currResults.SalesCommentLine.Length; scl++)
                {
                    if (currResults.SalesCommentLine[scl].DocNo == docNo)
                    {
                        if (currResults.SalesCommentLine[scl].Comment != "")
                        {
                            date = currResults.SalesCommentLine[scl].Date;
                            comment = currResults.SalesCommentLine[scl].Comment;
                            lineComments.Add(new Comment(date, comment));

                            date = string.Empty;
                            comment = string.Empty;
                        }
                    }
                }
            }

            return lineComments;
        }

        private void SetFunctionData()
        {
            List<ReturnReason> rrList = new List<ReturnReason>();
            List<PartRequestOptions> partReqOptions = new List<PartRequestOptions>();

            if (currResults.ReturnReasonCode != null)
            {
                for (int i = 0; i < currResults.ReturnReasonCode.Length; i++)
                {
                    rrList.Add(new ReturnReason(currResults.ReturnReasonCode[i].ReasonCode, currResults.ReturnReasonCode[i].Description,
                        currResults.ReturnReasonCode[i].Category));
                }
            }

            if (currResults.PartReqOption != null)
            {
                string[] split = currResults.PartReqOption[0].Split(',');

                for (int i = 0; i < split.Length; i++)
                {
                    partReqOptions.Add(new PartRequestOptions(split[i]));
                }
            }

            HttpContext.Current.Session["ReturnReasons"] = rrList;
            HttpContext.Current.Session["PartRequestOptions"] = partReqOptions;
        }

        public List<StatisticsSalesLine> GetStatisticsInformation()
        {
            List<StatisticsSalesLine> statLines = new List<StatisticsSalesLine>();
            Statistics stats = new Statistics();

            stats = webService.GetStatisticsInfo();

            string docType = string.Empty;
            string docNo = string.Empty;
            string externalDocNo = string.Empty;
            string itemNo = string.Empty;
            int qty = 0;
            string description = string.Empty;
            string createdDate = string.Empty;
            string customerNo = string.Empty;
            string reqReturnAction = string.Empty;
            bool isNotInvtAvailable = false;
            bool isOlderThan72Hours = false;
            bool isOlderThan48Hours = false;
            bool isOlderThan24Hours = false;
            bool isPendingSQApproval = false;
            bool custAllowRefund = false;
            string status = string.Empty;
            string exchangeOrderNo = string.Empty;
            bool noItemSubFound = false;
            string unitCost = string.Empty;
            string crossRefNo = string.Empty;

            if (stats.SalesLine != null)
            {
                for (int sl = 0; sl < stats.SalesLine.Length; sl++)
                {
                    docType = stats.SalesLine[sl].DocType;
                    docNo = stats.SalesLine[sl].DocNo;
                    externalDocNo = stats.SalesLine[sl].ExternalDocumentNo[0];
                    itemNo = stats.SalesLine[sl].ItemNo;

                    int.TryParse(stats.SalesLine[sl].Qty.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out qty);
                    description = stats.SalesLine[sl].Description;
                    createdDate = stats.SalesLine[sl].CreatedDate[0];
                    reqReturnAction = stats.SalesLine[sl].ReqReturnAction;
                    customerNo = stats.SalesLine[sl].CustomerNo;
                    isNotInvtAvailable = stats.SalesLine[sl].IsNotInvtAvailable[0] == "Yes" ? true : false;
                    isOlderThan72Hours = stats.SalesLine[sl].IsOlderThan72Hrs[0] == "Yes" ? true : false;
                    isOlderThan48Hours = stats.SalesLine[sl].IsOlderThan48Hrs[0] == "Yes" ? true : false;
                    isOlderThan24Hours = stats.SalesLine[sl].IsOlderThan24Hrs[0] == "Yes" ? true : false;
                    isPendingSQApproval = stats.SalesLine[sl].IsPendingSQApproval[0] == "Yes" ? true : false;
                    custAllowRefund = stats.SalesLine[sl].CustAllowRefund[0] == "Yes" ? true : false;
                    noItemSubFound = stats.SalesLine[sl].NoItemSubFound[0] == "Yes" ? true : false;
                    unitCost = stats.SalesLine[sl].UnitCost[0];
                    //crossRefNo = stats.SalesLine[sl].

                    if (isNotInvtAvailable)
                    {
                        status = "No inventory";
                    }

                    if (isOlderThan72Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 72 hours";
                        }
                        else
                        {
                            status += ", Is older than 72 hours";
                        }
                    }
                    else if (isOlderThan48Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 48 hours";
                        }
                        else
                        {
                            status += ", Is older than 48 hours";
                        }
                    }
                    else if (isOlderThan24Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 24 hours";
                        }
                        else
                        {
                            status += ", Is older than 24 hours";
                        }
                    }

                    if (isPendingSQApproval)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is pending SQ Approval";
                        }
                        else
                        {
                            status += ", Is pending SQ Approval";
                        }
                    }

                    if (noItemSubFound)
                    {
                        if (status.Length == 0)
                        {
                            status = "No Item Substitution Found";
                        }
                        else
                        {
                            status += ", No Item Substitution Found";
                        }
                    }

                    statLines.Add(new StatisticsSalesLine(docType, docNo, externalDocNo, itemNo, qty, description, createdDate, reqReturnAction, isNotInvtAvailable, isOlderThan72Hours,
                        isPendingSQApproval, customerNo, isOlderThan48Hours, status, isOlderThan24Hours, custAllowRefund, exchangeOrderNo, unitCost));

                    docType = string.Empty;
                    docNo = string.Empty;
                    externalDocNo = string.Empty;
                    itemNo = string.Empty;
                    qty = 0;
                    description = string.Empty;
                    createdDate = string.Empty;
                    reqReturnAction = string.Empty;
                    customerNo = string.Empty;
                    isNotInvtAvailable = false;
                    isOlderThan72Hours = false;
                    isOlderThan48Hours = false;
                    isOlderThan24Hours = false;
                    isPendingSQApproval = false;
                    custAllowRefund = false;
                    status = string.Empty;
                    exchangeOrderNo = string.Empty;
                    noItemSubFound = false;
                    unitCost = string.Empty;
                    crossRefNo = string.Empty;
                }
            }

            if (stats.ReturnsBuffer != null)
            {
                for (int rb = 0; rb < stats.ReturnsBuffer.Length; rb++)
                {
                    docType = "RETURNS BUFFER";
                    docNo = stats.ReturnsBuffer[rb].RMANo;
                    externalDocNo = stats.ReturnsBuffer[rb].ExtDocNo;
                    itemNo = stats.ReturnsBuffer[rb].ItemNo;
                    status = stats.ReturnsBuffer[rb].Status;
                    exchangeOrderNo = stats.ReturnsBuffer[rb].ExchangeOrderNo;

                    description = stats.ReturnsBuffer[rb].Description;
                    createdDate = stats.ReturnsBuffer[rb].DateCreated;
                    customerNo = stats.ReturnsBuffer[rb].CustomerNo;
                    isOlderThan72Hours = stats.ReturnsBuffer[rb].IsOlderThan72Hrs2[0] == "Yes" ? true : false;
                    isOlderThan48Hours = stats.ReturnsBuffer[rb].IsOlderThan48Hrs2[0] == "Yes" ? true : false;
                    isOlderThan24Hours = stats.ReturnsBuffer[rb].IsOlderThan24Hrs2[0] == "Yes" ? true : false;

                    if (isOlderThan72Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 72 hours";
                        }
                        else
                        {
                            status += ", Is older than 72 hours";
                        }
                    }
                    else if (isOlderThan48Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 48 hours";
                        }
                        else
                        {
                            status += ", Is older than 48 hours";
                        }
                    }
                    else if (isOlderThan24Hours)
                    {
                        if (status.Length == 0)
                        {
                            status = "Is older than 24 hours";
                        }
                        else
                        {
                            status += ", Is older than 24 hours";
                        }
                    }

                    statLines.Add(new StatisticsSalesLine(docType, docNo, externalDocNo, itemNo, qty, description, createdDate, reqReturnAction, isNotInvtAvailable, isOlderThan72Hours,
                        isPendingSQApproval, customerNo, isOlderThan48Hours, status, isOlderThan24Hours, custAllowRefund, exchangeOrderNo, unitCost));

                    docType = string.Empty;
                    docNo = string.Empty;
                    externalDocNo = string.Empty;
                    itemNo = string.Empty;
                    qty = 0;
                    description = string.Empty;
                    createdDate = string.Empty;
                    reqReturnAction = string.Empty;
                    customerNo = string.Empty;
                    isNotInvtAvailable = false;
                    isOlderThan72Hours = false;
                    isOlderThan48Hours = false;
                    isOlderThan24Hours = false;
                    isPendingSQApproval = false;
                    custAllowRefund = false;
                    status = string.Empty;
                    exchangeOrderNo = string.Empty;
                }
            }

            return statLines;
        }

        public List<Information> GetBuildInformation()
        {
            List<Information> buildInfo = new List<Information>();
            AboutObjects ao = new AboutObjects();

            int id = -1;
            string name = string.Empty;
            string versionList = string.Empty;
            string type = string.Empty;
            string date = string.Empty;
            string time = string.Empty;
            bool compiled = false;

            ao = webService.GetObjectInfo();

            if (ao.Object != null)
            {
                for (int obj = 0; obj < ao.Object.Length; obj++)
                {
                    id = ao.Object[obj].ID;
                    name = ao.Object[obj].Name;
                    versionList = ao.Object[obj].VersionList;
                    type = ao.Object[obj].Type;
                    date = ao.Object[obj].Date;
                    time = ao.Object[obj].Time;
                    compiled = ao.Object[obj].Compiled == "Yes" ? true : false;

                    buildInfo.Add(new Information(id, name, type, date, time, versionList, compiled));

                    id = -1;
                    name = string.Empty;
                    versionList = string.Empty;
                    type = string.Empty;
                    date = string.Empty;
                    time = string.Empty;
                    compiled = false;
                }
            }

            return buildInfo;
        }

        private List<PartialRefunded> GetPartialRefunds(string no, string docNo)
        {
            List<PartialRefunded> pr = new List<PartialRefunded>();

            string orderNo = string.Empty;
            string extDocNo = string.Empty;
            string itemNo = string.Empty;
            string description = string.Empty;
            string returnReason = string.Empty;
            double refundAmount = 0;
            double refundSalesTax = 0;
            double refundShippingTax = 0;


            if (currResults.ReturnsBuffer != null)
            {
                for (int rb = 0; rb < currResults.ReturnsBuffer.Length; rb++)
                {
                    if (currResults.ReturnsBuffer[rb].OrderNo == no && currResults.ReturnsBuffer[rb].ExtDocNo == docNo)
                    {
                        orderNo = currResults.ReturnsBuffer[rb].OrderNo;
                        extDocNo = currResults.ReturnsBuffer[rb].ExtDocNo;
                        itemNo = currResults.ReturnsBuffer[rb].ItemNo;
                        description = currResults.ReturnsBuffer[rb].Description;
                        returnReason = currResults.ReturnsBuffer[rb].ReturnReason;
                        double.TryParse(currResults.ReturnsBuffer[rb].RefundAmt.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out refundAmount);
                        double.TryParse(currResults.ReturnsBuffer[rb].TaxAmount.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out refundSalesTax);
                        double.TryParse(currResults.ReturnsBuffer[rb].ShippingTax.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out refundShippingTax);

                        pr.Add(new PartialRefunded(orderNo, extDocNo, itemNo, description, returnReason, refundAmount, refundSalesTax, refundShippingTax));
                    }

                    orderNo = string.Empty;
                    extDocNo = string.Empty;
                    itemNo = string.Empty;
                    description = string.Empty;
                    returnReason = string.Empty;
                    refundAmount = 0;
                    refundSalesTax = 0;
                    refundShippingTax = 0;
                }
            }

            return pr;
        }

        public List<Item> GetSuggestedSimilarItems(string itemNo, int suggestionOption)
        {
            List<Item> items = new List<Item>();
            SuggestSimilarItem ssi = new SuggestSimilarItem();

            ssi = webService.GetSuggestSimilarItems(itemNo, suggestionOption);

            string no = string.Empty;
            string desc = string.Empty;
            double unitPrice = 0.0;

            if (ssi.Item != null)
            {
                for (int i = 0; i < ssi.Item.Length; i++)
                {
                    no = ssi.Item[i].No;
                    desc = ssi.Item[i].Description;
                    double.TryParse(ssi.Item[i].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out unitPrice);

                    items.Add(new Item(no, desc, unitPrice));

                    no = string.Empty;
                    desc = string.Empty;
                    unitPrice = 0.0;
                }
            }

            return items;
        }

        private void GetZendeskTickets(ref List<Customer> cust)
        {
            foreach (Customer singleCust in cust)
            {
                // Initit all ticket objects
                foreach (SalesHeader salesHeadInit in singleCust.SalesHeader)
                {
                    if (salesHeadInit.Tickets == null)
                    {
                        salesHeadInit.Tickets = new List<Zendesk>();
                    }
                }

                foreach (ReturnHeader returnHeadInit in singleCust.ReturnHeaders)
                {
                    if (returnHeadInit.Tickets == null)
                    {
                        returnHeadInit.Tickets = new List<Zendesk>();
                    }
                }
            }

            foreach (Customer singleCust in cust)
            {
                foreach (SalesHeader salesHead in singleCust.SalesHeader)
                {
                    List<long?> salesTickets = new List<long?>();

                    if (salesHead.Tickets == null)
                    {
                        salesHead.Tickets = new List<Zendesk>();
                    }

                    if (salesHead.ExternalDocumentNo != "")
                    {
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(salesHead.ExternalDocumentNo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(salesHead.ExternalDocumentNo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(salesHead.ExternalDocumentNo, ref salesTickets));
                    }

                    if (salesHead.RMANo != "")
                    {
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(salesHead.RMANo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(salesHead.RMANo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(salesHead.RMANo, ref salesTickets));
                    }

                    if (salesHead.QuoteOrderNo != "")
                    {
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(salesHead.QuoteOrderNo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(salesHead.QuoteOrderNo, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(salesHead.QuoteOrderNo, ref salesTickets));
                    }

                    if (salesHead.EbayUserID != "")
                    {
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(salesHead.EbayUserID, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(salesHead.EbayUserID, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(salesHead.EbayUserID, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchRequestorTickets(salesHead.EbayUserID, ref salesTickets));
                    }

                    if (singleCust.Name != "")
                    {
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(singleCust.Name, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(singleCust.Name, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(singleCust.Name, ref salesTickets));
                        salesHead.Tickets.AddRange(ZendeskHelper.SearchRequestorTickets(singleCust.Name, ref salesTickets));
                    }

                    if (currResults.CustSvcLog != null)
                    {
                        CultureInfo culture = new CultureInfo("en-US");
                        string ticketNo = string.Empty;
                        string createdDate = null;
                        string updatedDate = null;
                        DateTime? createdDateTime = null;
                        DateTime? updateDateTime = null;
                        string subject = string.Empty;

                        for (int csl = 0; csl < currResults.CustSvcLog.Length; csl++)
                        {
                            if (salesHead.SalesOrderNo == currResults.CustSvcLog[csl].OrderNo || salesHead.SalesOrderNo == currResults.CustSvcLog[csl].SalesQuoteNo)
                            {
                                if (!salesTickets.Any(ticket => ticket.Equals(Convert.ToInt64(currResults.CustSvcLog[csl].ZendeskTicketNo))) && currResults.CustSvcLog[csl].ZendeskTicketNo != 0)
                                {
                                    ticketNo = currResults.CustSvcLog[csl].ZendeskTicketNo.ToString();
                                    createdDate = currResults.CustSvcLog[csl].CreatedDate;
                                    updatedDate = currResults.CustSvcLog[csl].UpdateDate;
                                    subject = currResults.CustSvcLog[csl].Subject;

                                    if (createdDate == "")
                                    {
                                        createdDateTime = null;
                                    }
                                    else
                                    {
                                        createdDateTime = Convert.ToDateTime(createdDate, culture);
                                    }

                                    if (updatedDate == "")
                                    {
                                        updateDateTime = null;
                                    }
                                    else
                                    {
                                        updateDateTime = Convert.ToDateTime(updatedDate, culture);
                                    }

                                    salesHead.Tickets.Add(new Zendesk(ticketNo, createdDateTime, updateDateTime, subject,
                                            string.Empty, string.Empty, true, null, null, null, null));
                                    salesTickets.Add(Convert.ToInt64(ticketNo));
                                }
                            }

                            ticketNo = string.Empty;
                            createdDate = null;
                            updatedDate = null;
                            subject = string.Empty;
                            createdDateTime = null;
                            updateDateTime = null;
                        }
                    }

                    foreach (ReturnHeader returnHead in singleCust.ReturnHeaders)
                    {
                        List<long?> returnTickets = new List<long?>();

                        if (returnHead.Tickets == null)
                        {
                            returnHead.Tickets = new List<Zendesk>();
                        }

                        if (returnHead.ExternalDocumentNo != "")
                        {
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(returnHead.ExternalDocumentNo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(returnHead.ExternalDocumentNo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(returnHead.ExternalDocumentNo, ref returnTickets));
                        }

                        if (returnHead.RMANo != "")
                        {
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(returnHead.RMANo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(returnHead.RMANo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(returnHead.RMANo, ref returnTickets));
                        }

                        if (returnHead.IMEINo != "")
                        {
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(returnHead.IMEINo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(returnHead.IMEINo, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(returnHead.IMEINo, ref returnTickets));
                        }

                        if (singleCust.Name != "")
                        {
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchCustomFieldTickets(singleCust.Name, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchSubjectTickets(singleCust.Name, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchDescriptionTickets(singleCust.Name, ref returnTickets));
                            returnHead.Tickets.AddRange(ZendeskHelper.SearchRequestorTickets(singleCust.Name, ref returnTickets));
                        }

                        if (currResults.CustSvcLog != null)
                        {
                            CultureInfo culture = new CultureInfo("en-US");
                            string ticketNo = string.Empty;
                            string createdDate = null;
                            string updatedDate = null;
                            DateTime? createdDateTime = null;
                            DateTime? updateDateTime = null;
                            string subject = string.Empty;

                            for (int csl = 0; csl < currResults.CustSvcLog.Length; csl++)
                            {
                                if (returnHead.RMANo == currResults.CustSvcLog[csl].RMANo)
                                {
                                    if (!returnTickets.Any(ticket => ticket.Equals(currResults.CustSvcLog[csl].ZendeskTicketNo)) && currResults.CustSvcLog[csl].ZendeskTicketNo != 0)
                                    {
                                        ticketNo = currResults.CustSvcLog[csl].ZendeskTicketNo.ToString();
                                        createdDate = currResults.CustSvcLog[csl].CreatedDate;
                                        updatedDate = currResults.CustSvcLog[csl].UpdateDate;
                                        subject = currResults.CustSvcLog[csl].Subject;

                                        if (createdDate == "")
                                        {
                                            createdDateTime = null;
                                        }
                                        else
                                        {
                                            createdDateTime = Convert.ToDateTime(createdDate, culture);
                                        }

                                        if (updatedDate == "")
                                        {
                                            updateDateTime = null;
                                        }
                                        else
                                        {
                                            updateDateTime = Convert.ToDateTime(updatedDate, culture);
                                        }

                                        returnHead.Tickets.Add(new Zendesk(ticketNo, createdDateTime, updateDateTime, subject,
                                            string.Empty, string.Empty, true, null, null, null, null));
                                        returnTickets.Add(Convert.ToInt64(ticketNo));
                                    }
                                }

                                ticketNo = string.Empty;
                                createdDate = null;
                                updatedDate = null;
                                subject = string.Empty;
                                createdDateTime = null;
                                updateDateTime = null;
                            }
                        } // if null
                    }
                }
            }
        }
    }
}