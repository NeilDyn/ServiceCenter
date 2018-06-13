using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class SendService
    {
        public List<Customer> CustomerList { get; set; }

        public void SetActiveCustomer(int custID)
        {
           HttpContext.Current.Session["ActiveCustomer"] = CustomerList[custID - 1];
        }
    }
}