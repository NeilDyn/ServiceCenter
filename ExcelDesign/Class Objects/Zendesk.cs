using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Zendesk
    {
        public string TicketNo { get; set; }

        public Zendesk()
        {

        }

        public Zendesk(string ticketNoP)
        {
            TicketNo = ticketNoP;
        }

        public string TicketLink()
        {            
            return String.Format("<a href = 'https://jegsons.zendesk.com/agent/tickets/" + TicketNo + "' target = '_blank' > #" + TicketNo + " </ a > ");
        }
    }
}