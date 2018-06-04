using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExcelDesign.ServiceFunctions;

namespace ExcelDesign.Class_Objects
{
    public class Customer
    {
        private string name;
        private string address1;
        private string address2;
        private string shipToContact;
        private string city;
        private string zip;
        private string state;
        private string country;
        private List<SalesHeader> salesHeaders;
        private List<ReturnHeader> returnHeaders;

        public List<ReturnHeader> ReturnHeaders
        {
            get { return returnHeaders; }
            set { returnHeaders = value; }
        }


        public List<SalesHeader> SalesHeader
        {
            get { return salesHeaders; }
            set { salesHeaders = value; }
        }


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
            this.Zip = zip;
            this.State = stateP;
            this.Country = countryP;
            this.SalesHeader = salesHeadersP;
            this.ReturnHeaders = returnHeadersP;
        }

        public string Country
        {
            get { return country; }
            set { country = value; }
        }
        

        public string State
        {
            get { return state; }
            set { state = value; }
        }
        

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
        

        public string City
        {
            get { return city; }
            set { city = value; }
        }
        

        public string ShipToContact
        {
            get { return shipToContact; }
            set { shipToContact = value; }
        }
        

        public string Address2
        {
            get { return address2; }
            set { address2 = value; }
        }
        

        public string Address1
        {
            get { return address1; }
            set { address1 = value; }
        }
        

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        
    }
}