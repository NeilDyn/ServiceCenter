using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class ReturnHeader
    {
        private string returnStatus;
        private string dateCreated;
        private string channelName;
        private List<ReceiptHeader> receiptHeaderObj;
        private List<PostedReceive> postedReceiveObj;
        private string returnTrackingNo;
        private string orderDate;
        private string rmaNo;
        private string externalDocumentNo;

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

        public string ExternalDocumentNo
        {
            get { return externalDocumentNo; }
            set { externalDocumentNo = value; }
        }
        

        public string RMANo
        {
            get { return rmaNo; }
            set { rmaNo = value; }
        }
        

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        

        public string ReturnTrackingNo
        {
            get { return returnTrackingNo; }
            set { returnTrackingNo = value; }
        }
        

        public List<PostedReceive> PostedReceiveObj
        {
            get { return postedReceiveObj; }
            set { postedReceiveObj = value; }
        }
        

        public List<ReceiptHeader> ReceiptHeaderObj
        {
            get { return receiptHeaderObj; }
            set { receiptHeaderObj = value; }
        }      

        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        

        public string DateCreated
        {
            get { return dateCreated; }
            set { dateCreated = value; }
        }
        

        public string ReturnStatus
        {
            get { return returnStatus; }
            set { returnStatus = value; }
        }
        
    }
}