using ExcelDesign.Class_Objects.CreatedExchange;
using ExcelDesign.Class_Objects.CreatedReturn;
using ExcelDesign.ServiceFunctions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
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

        public CreatedReturnHeader CreateReturnOrder(string orderNo, string externalDocumentNo, string returnReason, int defect, string notes,
            bool includeResource, bool printRMA, bool createLabel, string email, string lineValues, bool update)
        {
            ReturnOrder returnRMA = new ReturnOrder();
            CreatedReturnHeader cth = new CreatedReturnHeader();

            returnRMA = webService.CreateReturnOrder(orderNo, externalDocumentNo, returnReason, defect, notes, createLabel, printRMA, includeResource, email, lineValues, update);

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
            List<CreatedReturnLines> ctl = new List<CreatedReturnLines>();

            if (ro.SalesHeader != null)
            {
                rmaNo = ro.SalesHeader[0].No;
                externalDocumentNo = ro.SalesHeader[0].ExtDocNo;
                dateCreated = ro.SalesHeader[0].DocDate;
                channelName = ro.SalesHeader[0].SellToCustomerName;
                returnTrackingNo = ro.SalesHeader[0].ReturnTrackingNo;
                //orderDate = ro.SalesHeader[0]
                ctl = CreateReturnOrderLines(ro);

                crh.RMANo = rmaNo;
                crh.ExternalDocumentNo = externalDocumentNo;
                crh.DateCreated = dateCreated;
                crh.ChannelName = channelName;
                crh.ReturnTrackingNo = returnTrackingNo;
                crh.OrderDate = orderDate;
                crh.CreatedReturnLines = ctl;

                rmaNo = string.Empty;
                externalDocumentNo = string.Empty;
                dateCreated = string.Empty;
                channelName = string.Empty;
                returnTrackingNo = string.Empty;
                orderDate = string.Empty;
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

            if(ro.SalesLine != null)
            {
                for (int sl = 0; sl < ro.SalesLine.Length; sl++)
                {
                    int.TryParse(ro.SalesLine[sl].Qty, out quantity);
                    if (quantity > 0)
                    {
                        itemNo = ro.SalesLine[sl].ItemNo;
                        description = ro.SalesLine[sl].Description;

                        double.TryParse(ro.SalesLine[sl].UnitPrice.Replace(",", ""), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out price);
                        lineAmount = quantity * price;

                        ctl.Add(new CreatedReturnLines(itemNo, description, quantity, price, lineAmount));
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

        public string CreateExchange(string rmaNo)
        {
            CreatedExchangeOrder eo = new CreatedExchangeOrder();
            CreatedExchangeHeader ceh = new CreatedExchangeHeader();

            eo = webService.CreateExchange(rmaNo);

            ceh = CreateExchangeOrder(eo);
            HttpContext.Current.Session["CreatedExchange"] = ceh;
            return ceh.OrderNo;
        }

        public CreatedExchangeHeader CreateExchangeOrder(CreatedExchangeOrder eo)
        {
            CreatedExchangeHeader ceh = new CreatedExchangeHeader();
            string orderNo = string.Empty;
            string externalDocumentNo = string.Empty;
            string orderDate = string.Empty;
            string channelName = string.Empty;
            string shipMethod = string.Empty;
            string rmaNo = string.Empty;
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

                ceh.OrderNo = orderNo;
                ceh.ExternalDocumentNo = externalDocumentNo;
                ceh.OrderDate = orderDate;
                ceh.ChannelName = channelName;
                ceh.ShipMethod = shipMethod;
                ceh.RMANo = rmaNo;
                ceh.ExchangeLines = lines;

                orderNo = string.Empty;
                externalDocumentNo = string.Empty;
                orderDate = string.Empty;
                channelName = string.Empty;
                shipMethod = string.Empty;
                rmaNo = string.Empty;
                lines = new List<CreatedExchangeLines>();
            }

            return ceh;
        }

        public List<CreatedExchangeLines> CreateExchangeOrderLines(CreatedExchangeOrder eo)
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

        public string DeleteRMA(string rmaNo)
        {
            return webService.DeleteRMA(rmaNo);
        }

        public void IssueReturnLabel(string rmaNo, string email)
        {
            webService.IssueReturnLabel(rmaNo, email);
        }

        public void UpdateUserPassword(string currentUser, string newPassword)
        {
            webService.UpdateUserPassword(currentUser, newPassword);
        }

        public void ResetSession(string userID)
        {
            webService.ResetSession(userID);
        }
    }
}