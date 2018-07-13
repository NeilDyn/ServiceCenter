using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.CreatedExchange
{
    public class CreatedExchangeHeader
    {
        public string OrderNo { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string OrderDate { get; set; }
        public string ChannelName { get; set; }
        public string ShipMethod { get; set; }
        public string RMANo { get; set; }
        public List<CreatedExchangeLines> ExchangeLines { get; set; }

        public CreatedExchangeHeader(string orderNoP, string externalDocumentNoP, string orderDateP, string channelNameP, string shipMethodP, string rmaNoP,
            List<CreatedExchangeLines> exchangeLinesP)
        {
            OrderNo = orderNoP;
            ExternalDocumentNo = externalDocumentNoP;
            OrderDate = orderDateP;
            ChannelName = channelNameP;
            ShipMethod = shipMethodP;
            RMANo = rmaNoP;
            ExchangeLines = exchangeLinesP;
        }

        public CreatedExchangeHeader()
        {

        }
    }
}