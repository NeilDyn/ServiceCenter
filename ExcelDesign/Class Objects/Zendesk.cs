using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExcelDesign.Class_Objects
{
    public class Zendesk
    {
        private string ticket;
        private int ticketCount;

        public int TicketCount
        {
            get { return ticketCount; }
            set { ticketCount = value; }
        }
        

        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }
        
    }
}