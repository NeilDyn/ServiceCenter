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

                TableCell createdDate = new TableCell
                {
                    Text = ticket.CreatedDate.ToString(),
                    ToolTip = ticket.CreatedDate.ToString()
                };

                TableCell updatedDate = new TableCell
                {
                    Text = ticket.UpdatedDate.ToString(),
                    ToolTip = ticket.UpdatedDate.ToString()
                };

                TableCell subject = new TableCell
                {
                    Text = ticket.Subject,
                    ToolTip = ticket.Subject
                };

                TableCell status = new TableCell
                {
                    Text = ticket.Status,
                    ToolTip = ticket.Status
                };

                TableCell priority = new TableCell
                {
                    Text = ticket.Priority,
                    ToolTip = ticket.Priority
                };

                tr.Cells.Add(ticketID);
                tr.Cells.Add(createdDate);
                tr.Cells.Add(updatedDate);
                tr.Cells.Add(subject);
                tr.Cells.Add(status);
                tr.Cells.Add(priority);

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