using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ExcelDesign.Webservices
{
    /// <summary>
    /// Summary description for WebServiceFunctions
    /// </summary>
    [WebService(Namespace = "")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceFunctions : System.Web.Services.WebService
    {
        [WebMethod]
        public Zendesk GetTicketInformation(string ticketNo)
        {
            Zendesk ticket = new Zendesk();
            
            try
            {              
                long.TryParse(ticketNo, out long longTicketNo);

                ticket = ticket.VerifyInsertedZendeskTicket(longTicketNo);

                if (ticket != null)
                {
                    if (String.IsNullOrEmpty(ticket.FromEmailAddress) && String.IsNullOrEmpty(ticket.FromEmailName))
                    {
                        ticket.GetRequester();
                    }
                }
            }
            catch (Exception)
            {
                ticket = null;
            }

            return ticket;
        }
    }
}
