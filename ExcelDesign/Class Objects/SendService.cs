using ExcelDesign.ServiceFunctions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class SendService
    {
        public List<Customer> CustomerList { get; set; }

        private WebService webService;


        public void SetActiveCustomer(int custID)
        {
            HttpContext.Current.Session["ActiveCustomer"] = CustomerList[custID - 1];
        }

        public SendService()
        {
            webService = new WebService();
        }

        public string CreateReturnOrder(string orderNo, string externalDocumentNo, string returnReason, int defect, string notes,
            bool includeResource, bool printRMA, bool createLabel, string email)
        {
            string returnRMA;

            returnRMA = webService.CreateReturnOrder(orderNo, externalDocumentNo, returnReason, defect, notes, createLabel, printRMA, includeResource, string.Empty);

            return returnRMA;
        }

        
    }
}