﻿using System;
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

        public ReturnHeader()
        {

        }

        public ReturnHeader(string returnStatusP, string dateCreatedP, string channelNameP, List<ReceiptHeader> receiptHeaderObjP,
                 List<PostedReceive> postedReceiveObjP, string returnTrackingNoP, string orderDateP, string rmaNoP, string externalDocumentNoP, string emailP,
                 bool returnLabelCreatedP, bool exchangeCreatedP, List<string> exchangeOrderNoP, string sellToCustomerNoP, List<Comment> comments)
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
        }
    }
}