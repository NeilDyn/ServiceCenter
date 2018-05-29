using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class PostedPackage
    {
        private string trackingNo;

        public PostedPackage()
        {

        }

        public PostedPackage(string trackingNoP)
        {
            this.TrackingNo = trackingNoP;
        }

        public string TrackingNo
        {
            get { return trackingNo; }
            set { trackingNo = value; }
        }
        
    }
}