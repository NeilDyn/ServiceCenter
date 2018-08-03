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
        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToZip { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCountry { get; set; }

    public CreatedExchangeHeader(string orderNoP, string externalDocumentNoP, string orderDateP, string channelNameP, string shipMethodP, string rmaNoP,
            List<CreatedExchangeLines> exchangeLinesP, string shipToNameP, string shipToAddress1P, string shipToAddress2P, string shipToContactP, string shipToCityP,
            string shipToZipP, string shipToStateP, string shipToCountryP)
        {
            OrderNo = orderNoP;
            ExternalDocumentNo = externalDocumentNoP;
            OrderDate = orderDateP;
            ChannelName = channelNameP;
            ShipMethod = shipMethodP;
            RMANo = rmaNoP;
            ExchangeLines = exchangeLinesP;
            ShipToName = shipToNameP;
            ShipToAddress1 = shipToAddress1P;
            ShipToAddress2 = shipToAddress2P;
            ShipToContact = shipToContactP;
            ShipToCity = shipToCityP;
            ShipToZip = shipToZipP;
            ShipToState = shipToStateP;
            ShipToCountry = shipToCountryP;
        }

        public CreatedExchangeHeader()
        {

        }
    }
}