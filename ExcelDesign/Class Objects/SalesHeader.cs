using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class SalesHeader
    {
        private string orderStatus;
        private string orderDate;
        private string salesOrderNo;
        private string channelName;
        private List<ShipmentHeader> shipmentHeaderObject;
        private List<PostedPackage> postedPackageObject;
        //private ShipmentHeader shipmentHeaderObject;
        //private PostedPackage postedPackageObject;
        private string externalDocumentNo;

        public SalesHeader()
        {

        }

        public SalesHeader(string orderStatusP, string orderDateP, string salesOrderNoP, string channelNameP, List<ShipmentHeader> shipmentHeaderObjectP, List<PostedPackage> postedPackageObjectP, string externalDocumentNoP)
        {
            this.OrderStatus = orderStatusP;
            this.OrderDate = orderDateP;
            this.SalesOrderNo = salesOrderNoP;
            this.ChannelName = channelNameP;
            this.ShipmentHeaderObject = shipmentHeaderObjectP;
            this.PostedPackageObject = postedPackageObjectP;
            this.ExternalDocumentNo = externalDocumentNoP;
        }

        public string ExternalDocumentNo
        {
            get { return externalDocumentNo; }
            set { externalDocumentNo = value; }
        }

        public List<PostedPackage> PostedPackageObject
        {
            get { return postedPackageObject; }
            set { postedPackageObject = value; }
        }

        public List<ShipmentHeader> ShipmentHeaderObject
        {
            get { return shipmentHeaderObject; }
            set { shipmentHeaderObject = value; }
        }   
        

        //public List<PostedPackage> PostedPackageObject
        //{
        //    get { return postedPackageObject; }
        //    set { postedPackageObject = value; }
        //}
        

        //public List<ShipmentHeader> ShipmentHeaderObject
        //{
        //    get { return shipmentHeaderObject; }
        //    set { shipmentHeaderObject = value; }
        //}      
        

        public string ChannelName
        {
            get { return channelName; }
            set { channelName = value; }
        }
        

        public string SalesOrderNo
        {
            get { return salesOrderNo; }
            set { salesOrderNo = value; }
        }
        

        public string OrderDate
        {
            get { return orderDate; }
            set { orderDate = value; }
        }
        

        public string OrderStatus
        {
            get { return orderStatus; }
            set { orderStatus = value; }
        }
        
    }
}