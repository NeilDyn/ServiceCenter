using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects.CreatedPartRequest
{
    public class CreatedPartRequestHeader
    {
        public string QuoteNo { get; set; }
        public string ExternalDocumentNo { get; set; }
        public string QuoteDate { get; set; }
        public string ChannelName { get; set; }
        public string ShipMethod { get; set; }
        public string PartRequestOrderNo { get; set; }
        public List<CreatedPartRequestLines> PartRequestLines { get; set; }
        public string ShipToName { get; set; }
        public string ShipToAddress1 { get; set; }
        public string ShipToAddress2 { get; set; }
        public string ShipToContact { get; set; }
        public string ShipToCity { get; set; }
        public string ShipToZip { get; set; }
        public string ShipToState { get; set; }
        public string ShipToCountry { get; set; }

        public CreatedPartRequestHeader(string quoteNoP, string externalDocumentNoP, string quoteDateP, string channelNameP, string shipMethodP, string partRequestOrderNoP,
            List<CreatedPartRequestLines> partRequestLinesP, string shipToNameP, string shipToAddress1P, string shipToAddress2P, string shipToContactP, string shipToCityP,
            string shipToZipP, string shipToStateP, string shipToCountryP)
        {
            QuoteNo = quoteNoP;
            ExternalDocumentNo = externalDocumentNoP;
            QuoteDate = quoteDateP;
            ChannelName = channelNameP;
            ShipMethod = shipMethodP;
            PartRequestOrderNo = partRequestOrderNoP;
            PartRequestLines = partRequestLinesP;
            ShipToName = shipToNameP;
            ShipToAddress1 = shipToAddress1P;
            ShipToAddress2 = shipToAddress2P;
            ShipToContact = shipToContactP;
            ShipToCity = shipToCityP;
            ShipToZip = shipToZipP;
            ShipToState = shipToStateP;
            ShipToCountry = shipToCountryP;
        }

        public CreatedPartRequestHeader()
        {

        }
    }
}