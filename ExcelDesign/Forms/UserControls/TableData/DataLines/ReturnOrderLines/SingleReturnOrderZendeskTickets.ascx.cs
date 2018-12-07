using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.TableData.DataLines.ReturnOrderLines
{
    public partial class SingleReturnOrderZendeskTickets : System.Web.UI.UserControl
    {
        public List<Zendesk> ZendeskTickets { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void PopulateData()
        {
            int lineCount = 0;

            foreach (Zendesk ticket in ZendeskTickets)
            {
                lineCount++;

                TableRow tr = new TableRow();

                TableCell ticketID = new TableCell
                {
                    Text = ticket.TicketLink(),
                    ToolTip = ticket.TicketNo
                };

                tr.Cells.Add(ticketID);

                if (lineCount % 2 == 0)
                {
                    tr.BackColor = Color.White;
                }
                else
                {
                    tr.BackColor = ColorTranslator.FromHtml("#EFF3FB");
                }

                this.SingleReturnOrderZendeskTicketsTable.Rows.Add(tr);
            }
        }
    }
}