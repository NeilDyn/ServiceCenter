using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedReceive
    {
        private string trackingNo;
        private string packageNo;
        private string shippingAgent;
        private string shippingAgentService;
        private string receiveDate;
        private string sourceID;
        private string postedSourceID;
        private List<PostedReceiveLine> postedReceivesLines;

        public PostedReceive(string trackingNoP, string packageNoP, string shippingAgentP, string shippingAgentServiceP, string receiveDateP, string sourceIDP,
                    string postedSourceIDP, List<PostedReceiveLine> postedReceivesLinesP)
        {
            this.TrackingNo = trackingNoP;
            this.PackageNo = packageNoP;
            this.ShippingAgent = shippingAgentP;
            this.ShippingAgentService = shippingAgentServiceP;
            this.ReceiveDate = receiveDateP;
            this.SourceID = sourceIDP;
            this.PostedSourceID = postedSourceIDP;
            this.PostedReceiveLines = postedReceivesLinesP;
        }

        public string TrackingNo
        {
            get { return trackingNo; }
            set { trackingNo = value; }
        }

        public string PostedSourceID
        {
            get { return postedSourceID; }
            set { postedSourceID = value; }
        }


        public string SourceID
        {
            get { return sourceID; }
            set { sourceID = value; }
        }


        public string ReceiveDate
        {
            get { return receiveDate; }
            set { receiveDate = value; }
        }


        public string ShippingAgentService
        {
            get { return shippingAgentService; }
            set { shippingAgentService = value; }
        }


        public string ShippingAgent
        {
            get { return shippingAgent; }
            set { shippingAgent = value; }
        }

        public string PackageNo
        {
            get { return packageNo; }
            set { packageNo = value; }
        }
        public List<PostedReceiveLine> PostedReceiveLines
        {
            get { return postedReceivesLines; }
            set { postedReceivesLines = value; }
        }
    }
}