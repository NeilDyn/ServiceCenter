using ExcelDesign.Class_Objects.CreatedExchange;
using ExcelDesign.Class_Objects.CreatedPartRequest;
using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.Class_Objects.FunctionData;
using ExcelDesign.ServiceFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Added functions for Issue Refund, Cancel Order, Process Exchanges and Partial Refunds
     */

    /* v7.2 - 15 October 2018 - Neil Jansen
    * Updated Process Items function to added parameter type determing if processing replacements or refunds.
    */

    /* v9.2 - 12 December 2018 - Neil Jansen
     * Update CreateReturnOrders and PDA Return functions to pass Zendesk Ticket # Parameter
     * 
     * 13 December 2018 - Neil Jansen
     * Update CreateExchangeOrder to pass Zendesk Ticket # Parameter
     * Update CreatePartialRequest(Actuall a part request) to pass Zendesk Ticket # Parameter
     * Update PartialRefund to pass Zendesk Ticket # Parameter
     * Update CancelOrder to pass Zendesk Ticket # Parameter
     * Update IssueRefund to pass Zendesk Ticket # Parameter
     */

    public class SendService
    {
        public List<Customer> CustomerList { get; set; }

        private WebService webService;

        public void SetActiveCustomer(int custID)
        {
            HttpContext.Current.Session["ActiveCustomer"] = CustomerList[custID - 1];
        }

        public SendService()
        {
            webService = new WebService();
        }

        public CreatedReturnHeader CreateReturnOrder(string orderNo, string externalDocumentNo, string returnReason, string notes,
            bool includeResource, bool printRMA, bool createLabel, string email, string lineValues, bool update, string returnTrackingNo,
            string shippingDetails, string imeiNo, int zendeskTicketNo)
        {
            ReturnOrder returnRMA = new ReturnOrder();
            CreatedReturnHeader cth = new CreatedReturnHeader();

            /* v9.2 - 12 December 2018 - Neil Jansen */
            returnRMA = webService.CreateReturnOrder(orderNo, externalDocumentNo, returnReason, notes, includeResource, printRMA,
                createLabel, email, lineValues, update, returnTrackingNo, shippingDetails, imeiNo, zendeskTicketNo);

            cth = CreateReturnRMA(returnRMA);

            return cth;
        }

        protected CreatedReturnHeader CreateReturnRMA(ReturnOrder ro)
        {
            CreatedReturnHeader crh = new CreatedReturnHeader();
            string rmaNo = string.Empty;
            string externalDocumentNo = string.Empty;
            string dateCreated = string.Empty;
            string channelName = string.Empty;
            string returnTrackingNo = string.Empty;
            string orderDate = string.Empty;
            string imeiNo = string.Empty;
            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToCode = string.Empty;
            string shipToState = string.Empty;
            List<CreatedReturnLines> ctl = new List<CreatedReturnLines>();

            if (ro.SalesHeader != null)
            {
                rmaNo = ro.SalesHeader[0].No;
                externalDocumentNo = ro.SalesHeader[0].ExtDocNo;
                dateCreated = ro.SalesHeader[0].DocDate;
                channelName = ro.SalesHeader[0].SellToCustomerName;
                returnTrackingNo = ro.SalesHeader[0].ReturnTrackingNo;
                shipToName = ro.SalesHeader[0].ShipToName;
                shipToAddress1 = ro.SalesHeader[0].ShipToAddress;
                shipToAddress2 = ro.SalesHeader[0].ShipToAddress2;
                shipToContact = ro.SalesHeader[0].ShipToContact;
                shipToCity = ro.SalesHeader[0].ShipToCity;
                shipToCode = ro.SalesHeader[0].ShipToZip;
                shipToState = ro.SalesHeader[0].ShipToState;
                imeiNo = ro.SalesHeader[0].IMEI;

                ctl = CreateReturnOrderLines(ro);

                crh.RMANo = rmaNo;
                crh.ExternalDocumentNo = externalDocumentNo;
                crh.DateCreated = dateCreated;
                crh.ChannelName = channelName;
                crh.ReturnTrackingNo = returnTrackingNo;
                crh.OrderDate = orderDate;
                crh.CreatedReturnLines = ctl;
                crh.ShipToName = shipToName;
                crh.ShipToAddress1 = shipToAddress1;
                crh.ShipToAddress2 = shipToAddress2;
                crh.ShipToContact = shipToContact;
                crh.ShipToCity = shipToCity;
                crh.ShipToCode = shipToCode;
                crh.ShipToState = shipToState;
                crh.IMEINo = imeiNo;

                rmaNo = string.Empty;
                externalDocumentNo = string.Empty;
                dateCreated = string.Empty;
                channelName = string.Empty;
                returnTrackingNo = string.Empty;
                orderDate = string.Empty;
                imeiNo = string.Empty;
                shipToName = string.Empty;
                shipToAddress1 = string.Empty;
                shipToAddress2 = string.Empty;
                shipToContact = string.Empty;
                shipToCity = string.Empty;
                shipToCode = string.Empty;
                shipToState = string.Empty;
                ctl = new List<CreatedReturnLines>();
            }

            return crh;
        }

        protected List<CreatedReturnLines> CreateReturnOrderLines(ReturnOrder ro)
        {
            List<CreatedReturnLines> ctl = new List<CreatedReturnLines>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            double price = 0;
            double lineAmount = 0;
            string reqReturnAction = string.Empty;
            string returnReason = string.Empty;

            List<ReturnReason> rrList = (List<ReturnReason>)HttpContext.Current.Session["ReturnReasons"];

            if (ro.SalesLine != null)
            {
                for (int sl = 0; sl < ro.SalesLine.Length; sl++)
                {
                    int.TryParse(ro.SalesLine[sl].Qty, out quantity);
                    if (quantity > 0)
                    {
                        itemNo = ro.SalesLine[sl].ItemNo;
                        description = ro.SalesLine[sl].Description;
                        reqReturnAction = ro.SalesLine[sl].REQReturnAction;

                        foreach (ReturnReason rr in rrList)
                        {
                            if(rr.ReasonCode == ro.SalesLine[sl].ReturnReasonCode)
                            {
                                returnReason = rr.Display;
                            }
                        }

                        double.TryParse(ro.SalesLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        ctl.Add(new CreatedReturnLines(itemNo, description, quantity, price, lineAmount, reqReturnAction, returnReason));
                    }

                    itemNo = string.Empty;
                    description = string.Empty;
                    quantity = 0;
                    price = 0;
                    lineAmount = 0;
                }
            }

            return ctl;
        }

        public CreatedExchangeHeader CreateExchangeOrder(string rmaNo, string externalDocumentNo, string lineValues, int zendeskTicketNo)
        {
            CreatedExchangeOrder ceo = new CreatedExchangeOrder();
            CreatedExchangeHeader ceh = new CreatedExchangeHeader();

            ceo = webService.CreateExchange(rmaNo, externalDocumentNo, lineValues, zendeskTicketNo);

            ceh = CreateExchange(ceo);
            return ceh;
        }

        protected CreatedExchangeHeader CreateExchange(CreatedExchangeOrder eo)
        {
            CreatedExchangeHeader ceh = new CreatedExchangeHeader();
            string orderNo = string.Empty;
            string externalDocumentNo = string.Empty;
            string orderDate = string.Empty;
            string channelName = string.Empty;
            string shipMethod = string.Empty;
            string rmaNo = string.Empty;
            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToZip = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;
            List<CreatedExchangeLines> lines = new List<CreatedExchangeLines>();

            if(eo.SalesHeader != null)
            {
                orderNo = eo.SalesHeader[0].No;
                externalDocumentNo = eo.SalesHeader[0].ExtDocNo;
                orderDate = eo.SalesHeader[0].DocDate;
                channelName = eo.SalesHeader[0].SellToCustomerName;
                shipMethod = eo.SalesHeader[0].ShippingAgent;
                shipMethod += " " + eo.SalesHeader[0].ShippingService;
                rmaNo = eo.SalesHeader[0].RMANo1;
                lines = CreateExchangeOrderLines(eo);
                shipToName = eo.SalesHeader[0].ShipToName;
                shipToAddress1 = eo.SalesHeader[0].ShipToAddress;
                shipToAddress2 = eo.SalesHeader[0].ShipToAddress2;
                shipToContact = eo.SalesHeader[0].ShipToContact;
                shipToCity = eo.SalesHeader[0].ShipToCity;
                shipToZip = eo.SalesHeader[0].ShipToZip;
                shipToState = eo.SalesHeader[0].ShipToState;
                shipToCountry = eo.SalesHeader[0].ShipToCountry;

                ceh.OrderNo = orderNo;
                ceh.ExternalDocumentNo = externalDocumentNo;
                ceh.OrderDate = orderDate;
                ceh.ChannelName = channelName;
                ceh.ShipMethod = shipMethod;
                ceh.RMANo = rmaNo;
                ceh.ExchangeLines = lines;
                ceh.ShipToName = shipToName;
                ceh.ShipToAddress1 = shipToAddress1;
                ceh.ShipToAddress2 = shipToAddress2;
                ceh.ShipToContact = shipToContact;
                ceh.ShipToCity = shipToCity;
                ceh.ShipToZip = shipToZip;
                ceh.ShipToState = shipToState;
                ceh.ShipToCountry = shipToCountry;

                orderNo = string.Empty;
                externalDocumentNo = string.Empty;
                orderDate = string.Empty;
                channelName = string.Empty;
                shipMethod = string.Empty;
                rmaNo = string.Empty;
                shipToAddress1 = string.Empty;
                shipToAddress2 = string.Empty;
                shipToContact = string.Empty;
                shipToCity = string.Empty;
                shipToZip = string.Empty;
                shipToState = string.Empty;
                shipToCountry = string.Empty;
                lines = new List<CreatedExchangeLines>();
            }

            return ceh;
        }

        protected List<CreatedExchangeLines> CreateExchangeOrderLines(CreatedExchangeOrder eo)
        {
            List<CreatedExchangeLines> cel = new List<CreatedExchangeLines>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            double price = 0;
            double lineAmount = 0;

            if (eo.SalesLine != null)
            {
                for (int sl = 0; sl < eo.SalesLine.Length; sl++)
                {
                    int.TryParse(eo.SalesLine[sl].Qty, out quantity);
                    if (quantity > 0)
                    {
                        itemNo = eo.SalesLine[sl].ItemNo;
                        description = eo.SalesLine[sl].Description;

                        double.TryParse(eo.SalesLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        cel.Add(new CreatedExchangeLines(itemNo, description, quantity, price, lineAmount));
                    }

                    itemNo = string.Empty;
                    description = string.Empty;
                    quantity = 0;
                    price = 0;
                    lineAmount = 0;
                }
            }

            return cel;
        }

        public CreatedPartRequestHeader CreatePartialRequest(string orderNo, string exernalDocumentNo, string lineDetails, string notes,
            string shippingDetails, string email, int zendeskTicketNo)
        {
            CreatedPartialRequest cpr = new CreatedPartialRequest();
            CreatedPartRequestHeader cprh = new CreatedPartRequestHeader();

            cpr = webService.CreatePartRequest(orderNo, exernalDocumentNo, lineDetails, notes, shippingDetails, email, zendeskTicketNo);

            cprh = CreatePartialRequestHeader(cpr);
            return cprh;
        }

        protected CreatedPartRequestHeader CreatePartialRequestHeader(CreatedPartialRequest cp)
        {
            CreatedPartRequestHeader cprh = new CreatedPartRequestHeader();
            string quoteNo = string.Empty;
            string externalDocumentNo = string.Empty;
            string quoteDate = string.Empty;
            string channelName = string.Empty;
            string shipMethod = string.Empty;
            string partReqOrderNo = string.Empty;
            string shipToName = string.Empty;
            string shipToAddress1 = string.Empty;
            string shipToAddress2 = string.Empty;
            string shipToContact = string.Empty;
            string shipToCity = string.Empty;
            string shipToZip = string.Empty;
            string shipToState = string.Empty;
            string shipToCountry = string.Empty;
            List<CreatedPartRequestLines> lines = new List<CreatedPartRequestLines>();

            if (cp.SalesHeader != null)
            {
                quoteNo = cp.SalesHeader[0].No;
                externalDocumentNo = cp.SalesHeader[0].ExtDocNo;
                quoteDate = cp.SalesHeader[0].DocDate;
                channelName = cp.SalesHeader[0].SellToCustomerName;
                shipMethod = cp.SalesHeader[0].ShippingAgent;
                shipMethod += " " + cp.SalesHeader[0].ShippingService;
                lines = CreatePartialRequestLines(cp);
                shipToName = cp.SalesHeader[0].ShipToName;
                shipToAddress1 = cp.SalesHeader[0].ShipToAddress;
                shipToAddress2 = cp.SalesHeader[0].ShipToAddress2;
                shipToContact = cp.SalesHeader[0].ShipToContact;
                shipToCity = cp.SalesHeader[0].ShipToCity;
                shipToZip = cp.SalesHeader[0].ShipToZip;
                shipToState = cp.SalesHeader[0].ShipToState;
                shipToCountry = cp.SalesHeader[0].ShipToCountry;

                if(cp.ExtendedSalesHeader != null)
                {
                    for (int esh = 0; esh < cp.ExtendedSalesHeader.Length; esh++)
                    {
                        if(cp.ExtendedSalesHeader[esh].QuoteNo == quoteNo)
                        {
                            partReqOrderNo = cp.ExtendedSalesHeader[esh].PartRequestOrderNo;
                        }
                    }
                }

                cprh.QuoteNo = quoteNo;
                cprh.ExternalDocumentNo = externalDocumentNo;
                cprh.QuoteDate = quoteDate;
                cprh.ChannelName = channelName;
                cprh.ShipMethod = shipMethod;
                cprh.PartRequestOrderNo = partReqOrderNo;
                cprh.PartRequestLines = lines;
                cprh.ShipToName = shipToName;
                cprh.ShipToAddress1 = shipToAddress1;
                cprh.ShipToAddress2 = shipToAddress2;
                cprh.ShipToContact = shipToContact;
                cprh.ShipToCity = shipToCity;
                cprh.ShipToZip = shipToZip;
                cprh.ShipToState = shipToState;
                cprh.ShipToCountry = shipToCountry;

                quoteNo = string.Empty;
                externalDocumentNo = string.Empty;
                quoteDate = string.Empty;
                channelName = string.Empty;
                shipMethod = string.Empty;
                partReqOrderNo = string.Empty;
                shipToAddress1 = string.Empty;
                shipToAddress2 = string.Empty;
                shipToContact = string.Empty;
                shipToCity = string.Empty;
                shipToZip = string.Empty;
                shipToState = string.Empty;
                shipToCountry = string.Empty;
                lines = new List<CreatedPartRequestLines>();
            }


            return cprh;
        }

        protected List<CreatedPartRequestLines> CreatePartialRequestLines(CreatedPartialRequest cp)
        {
            List<CreatedPartRequestLines> cprl = new List<CreatedPartRequestLines>();

            string itemNo = string.Empty;
            string description = string.Empty;
            int quantity = 0;
            double price = 0;
            double lineAmount = 0;

            if (cp.SalesLine != null)
            {
                for (int sl = 0; sl < cp.SalesLine.Length; sl++)
                {
                    int.TryParse(cp.SalesLine[sl].Qty, out quantity);
                    if (quantity > 0)
                    {
                        itemNo = cp.SalesLine[sl].ItemNo;
                        description = cp.SalesLine[sl].Description;

                        double.TryParse(cp.SalesLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        cprl.Add(new CreatedPartRequestLines(itemNo, description, quantity, price, lineAmount));
                    }

                    itemNo = string.Empty;
                    description = string.Empty;
                    quantity = 0;
                    price = 0;
                    lineAmount = 0;
                }
            }

            return cprl;
        }

        public string DeleteRMA(string rmaNo)
        {
            return webService.DeleteRMA(rmaNo);
        }

        public void IssueReturnLabel(string rmaNo, string email, string sessionID)
        {
            webService.IssueReturnLabel(rmaNo, email, sessionID);
        }

        public void UpdateUserPassword(string currentUser, string newPassword)
        {
            webService.UpdateUserPassword(currentUser, newPassword);
        }

        public void ResetSession(string userID)
        {
            webService.ResetSession(userID);
        }

        public void IssueRefund(string rmaNo, string sessionID, int zendeskTicketNo)
        {
            webService.IssueRefund(rmaNo, sessionID, zendeskTicketNo);
        }      

        public void CancelOrder(string orderNo, string docNo, string lineValues, int zendeskTicketNo)
        {
            webService.CancelOrder(orderNo, docNo, lineValues, zendeskTicketNo);
        }

        public void ProcessItems(string rmaList, string sessionID, string type)
        {
            webService.ProcessItems(rmaList, sessionID, type);
        }

        public void PartialRefund(string orderNo, string docNo, string lineValues, int zendeskTicketNo)
        {
            webService.PartialRefund(orderNo, docNo, lineValues, zendeskTicketNo);
        }

        public void UpdateREQReturnAction(string rmaList, string sessionID)
        {
            webService.UpdateREQReturnAction(rmaList, sessionID);
        }

        public void ProcessSuggestSimilarItems(string suggestionList, string sessionID)
        {
            webService.ProcessSuggestSimilarItems(suggestionList, sessionID);
        }
    }
}
 