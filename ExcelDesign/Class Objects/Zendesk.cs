using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Zendesk
    {
        public string TicketNo { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Subject { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }

        public Zendesk()
        {

        }

        public Zendesk(string ticketNoP, DateTime? createdDateP, DateTime? updatedDateP, string subjectP, string statusP, string priorityP)
        {
            TicketNo = ticketNoP;
            CreatedDate = createdDateP;
            UpdatedDate = updatedDateP;
            Subject = subjectP;
            Status = statusP;
            Priority = priorityP;
        }

        public Zendesk(string ticketNoP)
        {
            TicketNo = ticketNoP;
        }

        public string TicketLink()
        {            
            
            return TicketNo == "0" ? TicketNo : String.Format("<a href = 'https://jegsons.zendesk.com/agent/tickets/" + TicketNo + "' target = '_blank' > #" + TicketNo + " </ a > ");
        }
    }
}