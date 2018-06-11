using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackage
    {
        public string TrackingNo { get; set; }
        public List<PostedPackageLine> PostedPackageLines { get; set; }
        public string PostedSourceID { get; set; }
        public string SourceID { get; set; }
        public string PackingDate { get; set; }
        public string ShippingAgentService { get; set; }
        public string ShippingAgent { get; set; }
        public string PackageNo { get; set; }

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
            this.PackingDate = packDateP;
            this.SourceID = sourceIDP;
            this.PostedSourceID = postedSourceIDP;
            this.PostedPackageLines = postedPackageLinesP;
        }
    }
}