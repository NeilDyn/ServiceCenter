using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedReceive
    {
        public string TrackingNo { get; set; }
        public string PostedSourceID { get; set; }
        public string SourceID { get; set; }
        public string ReceiveDate { get; set; }
        public string ShippingAgentService { get; set; }
        public string ShippingAgent { get; set; }
        public string PackageNo { get; set; }
        public List<PostedReceiveLine> PostedReceiveLines { get; set; }

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
    }
}