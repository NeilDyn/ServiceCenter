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

        public ReturnHeader()
        {

        }

        public ReturnHeader(string returnStatusP, string dateCreatedP, string channelNameP, List<ReceiptHeader> receiptHeaderObjP,
                 List<PostedReceive> postedReceiveObjP, string returnTrackingNoP, string orderDateP, string rmaNoP, string externalDocumentNoP)
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
        }
    }
}