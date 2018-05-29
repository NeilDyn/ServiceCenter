using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackage
    {
        private string trackingNo;
        private string packageNo;
        private string shippingAgent;
        private string shippingAgentService;
        private string packDate;
        private string sourceID;
        private string postedSourceID;
        private List<PostedPackageLine> postedPackageLines;

        public PostedPackage()
        {

        }

        public PostedPackage(string trackingNoP, string packageNoP, string shippingAgentP, string shippingAgentServiceP, string packDateP, string sourceIDP, 
            string postedSourceIDP, List<PostedPackageLine> postedPackageLinesP)
        {
            this.TrackingNo = trackingNoP;
            this.PackageNo = packageNoP;
            this.ShippingAgent = shippingAgentP;
            this.ShippingAgentService = shippingAgentServiceP;
            this.packDate = packDateP;
            this.SourceID = sourceIDP;
            this.PostedSourceID = postedSourceIDP;
            this.PostedPackageLines = postedPackageLinesP;
        }

        public string TrackingNo
        {
            get { return trackingNo; }
            set { trackingNo = value; }
        }

        public List<PostedPackageLine> PostedPackageLines
        {
            get { return postedPackageLines; }
            set { postedPackageLines = value; }
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


        public string PackingDate
        {
            get { return packDate; }
            set { packDate = value; }
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
        
        
    }
}