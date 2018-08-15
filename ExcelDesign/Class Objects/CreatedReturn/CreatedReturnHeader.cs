using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.CreatedReturn
{
    public class CreatedReturnHeader
    {
        public string RMANo { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string DateCreated { get; set; }
        public string ChannelName { get; set; }
        public string ReturnTrackingNo { get; set; }
        public string OrderDate { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToCode { get; set; }
        public string ShipToState { get; set; }
        public List<CreatedReturnLines> CreatedReturnLines { get; set; }

        public CreatedReturnHeader(string rmaNoP, string externalDocNoP, string dateCreatedP, string channelNameP, string returnTrackingNoP, string orderDateP,
                                   List<CreatedReturnLines> createdReturnLinesP, string shipToNameP, string shipToAddress1P, string shipToAddress2P, string shipToContactP, 
                                   string shipToCityP, string shipToZipP, string shipToStateP)
        {
            RMANo = rmaNoP;
            ExternalDocumentNo = externalDocNoP;
            DateCreated = dateCreatedP;
            ChannelName = channelNameP;
            ReturnTrackingNo = returnTrackingNoP;
            OrderDate = orderDateP;
            CreatedReturnLines = createdReturnLinesP;
            ShipToName = shipToNameP;
            ShipToAddress1 = shipToAddress1P;
            ShipToAddress2 = shipToAddress2P;
            ShipToContact = shipToContactP;
            ShipToCity = shipToCityP;
            ShipToCode = shipToZipP;
            ShipToState = shipToStateP;
        }

        public CreatedReturnHeader()
        {

        }
    }
}