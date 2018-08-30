using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class SalesHeader
    {
        public string ExternalDocumentNo { get; set; }
        public List<PostedPackage> PostedPackageObject { get; set; }
        public List<ShipmentHeader> ShipmentHeaderObject { get; set; }
        public Warranty WarrantyProp { get; set; }
        public string ChannelName { get; set; }
        public string SalesOrderNo { get; set; }
        public string OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string RMANo { get; set; }
        public bool IsExchangeOrder { get; set; }
        public bool IsPartRequest { get; set; }
        public string SellToCustomerNo { get; set; }
        public List<Comment> OrderComments { get; set; }
        public string QuoteOrderNo { get; set; }

        public SalesHeader()
        {

        }

        public SalesHeader(string orderStatusP, string orderDateP, string salesOrderNoP, string channelNameP, List<ShipmentHeader> shipmentHeaderObjectP,
            List<PostedPackage> postedPackageObjectP, string externalDocumentNoP, Warranty _warrantyP, bool rmaExistsP, string rmaNoP, bool isExchangeOrderP,
            string sellToCustomerNoP, List<Comment> orderCommentsP, bool isPartRequest, string quoteOrderNoP)
        {
            this.OrderStatus = orderStatusP;
            this.OrderDate = orderDateP;
            this.SalesOrderNo = salesOrderNoP;
            this.ChannelName = channelNameP;
            this.ShipmentHeaderObject = shipmentHeaderObjectP;
            this.PostedPackageObject = postedPackageObjectP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.WarrantyProp = _warrantyP;
            this.RMANo = rmaNoP;
            this.IsExchangeOrder = isExchangeOrderP;
            this.SellToCustomerNo = sellToCustomerNoP;
            this.OrderComments = orderCommentsP;
            this.IsPartRequest = isPartRequest;
            this.QuoteOrderNo = quoteOrderNoP;
        }
    }
}