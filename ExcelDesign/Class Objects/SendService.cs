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

        public string DeleteRMA(string rmaNo)
        {
            return webService.DeleteRMA(rmaNo);
        }
    }
}