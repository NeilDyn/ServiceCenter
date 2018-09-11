using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class PendingSQApproval : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> SQList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void PopulateData()
        {
            try
            {
                TableRow breakRow = new TableRow();
                TableCell breakCell = new TableCell
                {
                    Text = "<br />"
                };

                breakRow.Cells.Add(breakCell);
                tblPendingSQApproval.Rows.Add(breakRow);

                int invNotAvail = 0;
                int olderThan72Hours = 0;
                int olderThan48Hours = 0;

                foreach (StatisticsSalesLine line in SQList)
                {
                    if (line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                    }
                    else if (line.IsOlderThan48Hours)
                    {
                        olderThan48Hours++;
                    }

                    if (line.IsNotInvtAvailable)
                    {
                        invNotAvail++;
                    }
                }

                if (olderThan72Hours > 0)
                {
                    tcSQOlderThan72Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"PendingSQApproval\", \"72Hours\")'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcSQOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    tcSQNoInvAvail.Text = "<a href='javascript:OpenSalesLineWindow(\"PendingSQApproval\", \"NoInventory\")'>" + invNotAvail.ToString() + "</a>";
                }
                else
                {
                    tcSQNoInvAvail.Text = invNotAvail.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcSQOlderThan48Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"PendingSQApproval\", \"48Hours\")'>" + olderThan48Hours.ToString() + "</a>";
                }
                else
                {
                    tcSQOlderThan48Hours.Text = olderThan48Hours.ToString();
                }

                if(SQList.Count > 0)
                {
                    tcAllSQPending.Text = "<a href='javascript:OpenSalesLineWindow(\"PendingSQApproval\", \"AllPending\")'>" + SQList.Count.ToString() + "</a>";
                }
                else
                {
                    tcAllSQPending.Text = SQList.Count.ToString();
                }

            }
            catch (Exception ex)
            {
                Session["Error"] = ex.Message;

                if (Request.Url.AbsoluteUri.Contains("Forms"))
                {
                    Response.Redirect("ErrorForm.aspx");
                }
                else
                {
                    Response.Redirect("Forms/ErrorForm.aspx");
                }
            }
        }

    }
}