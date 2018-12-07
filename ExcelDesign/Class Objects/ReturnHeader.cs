using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReturnHeader
    {
        public string ExternalDocumentNo { get; set; }
        public string RMANo { get; set; }
        public string OrderDate { get; set; }
        public string ReturnTrackingNo { get; set; }
        public List<PostedReceive> PostedReceiveObj { get; set; }
        public List<ReceiptHeader> ReceiptHeaderObj { get; set; }
        public string ChannelName { get; set; }
        public string DateCreated { get; set; }
        public string ReturnStatus { get; set; }
        public string Email { get; set; }
        public bool ReturnLabelCreated { get; set; }
        public bool ExchangeCreated { get; set; }
        public List<string> ExchangeOrderNo { get; set; }
        public string SellToCustomerNo { get; set; }
        public List<Comment> ReturnComments { get; set; }
        public string IMEINo { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToCode { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCountry { get; set; }
        public List<Zendesk> Tickets { get; set; }

        public ReturnHeader()
        {

        }

        public ReturnHeader(string returnStatusP, string dateCreatedP, string channelNameP, List<ReceiptHeader> receiptHeaderObjP,
                 List<PostedReceive> postedReceiveObjP, string returnTrackingNoP, string orderDateP, string rmaNoP, string externalDocumentNoP, string emailP,
                 bool returnLabelCreatedP, bool exchangeCreatedP, List<string> exchangeOrderNoP, string sellToCustomerNoP, List<Comment> comments, string imeiNoP,
                 string shipToNameP, string shipToAddress1P, string shipToAddress2P, string shipToContactP, string shipToCityP, string shipToZipP, string shipToStateP,
                 string shipToCountryP)
        {
            this.ReturnStatus = returnStatusP;
            this.DateCreated = dateCreatedP;
            this.ChannelName = channelNameP;
            this.ReceiptHeaderObj = receiptHeaderObjP;
            this.PostedReceiveObj = postedReceiveObjP;
            this.ReturnTrackingNo = returnTrackingNoP;
            this.OrderDate = orderDateP;
            this.RMANo = rmaNoP;
            this.ExternalDocumentNo = externalDocumentNoP;
            this.Email = emailP;
            this.ReturnLabelCreated = returnLabelCreatedP;
            this.ExchangeCreated = exchangeCreatedP;
            this.ExchangeOrderNo = exchangeOrderNoP;
            this.SellToCustomerNo = sellToCustomerNoP;
            this.ReturnComments = comments;
            this.IMEINo = imeiNoP;
            this.ShipToName = shipToNameP;
            this.ShipToAddress1 = shipToAddress1P;
            this.ShipToAddress2 = shipToAddress2P;
            this.ShipToContact = shipToContactP;
            this.ShipToCity = shipToCityP;
            this.ShipToCode = shipToZipP;
            this.ShipToState = shipToStateP;
            this.ShipToCountry = shipToCountryP;
        }
    }
}