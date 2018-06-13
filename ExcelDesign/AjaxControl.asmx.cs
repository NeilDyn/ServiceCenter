using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ExcelDesign.Class_Objects;

namespace ExcelDesign
{
    /// <summary>
    /// Summary description for AjaxControl
    /// </summary>
    [WebService(Namespace = "")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AjaxControl : System.Web.Services.WebService
    {

        [WebMethod]
        public void SetActiveCustomer(List<Customer> customers, int custID)
        {
            Session["ActiveCustomer"] = customers[custID - 1];
        }
    }
}
