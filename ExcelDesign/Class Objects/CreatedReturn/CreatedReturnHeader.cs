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
        public List<CreatedReturnLines> CreatedReturnLines { get; set; }

        public CreatedReturnHeader(string rmaNoP, string externalDocNoP, string dateCreatedP, string channelNameP, string returnTrackingNoP, string orderDateP,
                                   List<CreatedReturnLines> createdReturnLinesP)
        {
            RMANo = rmaNoP;
            ExternalDocumentNo = externalDocNoP;
            DateCreated = dateCreatedP;
            ChannelName = channelNameP;
            ReturnTrackingNo = returnTrackingNoP;
            OrderDate = orderDateP;
            CreatedReturnLines = createdReturnLinesP;
        }

        public CreatedReturnHeader()
        {

        }
    }
}