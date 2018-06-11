using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelDesign.ServiceFunctions;

namespace ExcelDesign.Class_Objects
{
    public class Customer
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string ShipToContact { get; set; }
        public string Address2 { get; set; }
        public string Address1 { get; set; }
        public string Name { get; set; }
        public List<ReturnHeader> ReturnHeaders { get; set; }
        public List<SalesHeader> SalesHeader { get; set; }

        public Customer()
        {

        }

        public Customer(string nameP, string address1P, string address2P, string shipToContactP, string cityP, string zipP, string stateP, string countryP,
            List<SalesHeader> salesHeadersP, List<ReturnHeader> returnHeadersP)
        {
            this.Name = nameP;
            this.Address1 = address1P;
            this.Address2 = address2P;
            this.ShipToContact = shipToContactP;
            this.City = cityP;
            this.Zip = Zip;
            this.State = stateP;
            this.Country = countryP;
            this.SalesHeader = salesHeadersP;
            this.ReturnHeaders = returnHeadersP;
        }
    }
}