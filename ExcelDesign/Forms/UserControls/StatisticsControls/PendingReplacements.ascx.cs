using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    /* v7.1 - 3 October 2018 - Neil Jansen
     * Added logic for Is Older than 24 Hours bucket
     */

    public partial class PendingReplacements : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> ReplacementList { get; set; }


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
                tblPendingReplacements.Rows.Add(breakRow);

                int invNotAvail = 0;
                int olderThan72Hours = 0;
                int olderThan48Hours = 0;
                int olderThan24Hours = 0;

                foreach (StatisticsSalesLine line in ReplacementList)
                {
                    if (line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                    }
                    else if (line.IsOlderThan48Hours)
                    {
                        olderThan48Hours++;
                    }
                    else if (line.IsOlderThan24Hours)
                    {
                        olderThan24Hours++;
                    }

                    if (line.IsNotInvtAvailable)
                    {
                        invNotAvail++;
                    }

                }

                if (olderThan72Hours > 0)
                {
                    tcReplacementOlderThan72Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Replacement\", \"72Hours\")'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcReplacementOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    tcReplacementNoInvAvail.Text = "<a href='javascript:OpenSalesLineWindow(\"Replacement\", \"NoInventory\")'>" + invNotAvail.ToString() + "</a>";
                }
                else
                {
                    tcReplacementNoInvAvail.Text = invNotAvail.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcReplacementOlderThan48Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Replacement\", \"48Hours\")'>" + olderThan48Hours.ToString() + "</a>";
                }
                else
                {
                    tcReplacementOlderThan48Hours.Text = olderThan48Hours.ToString();
                }

                if (olderThan24Hours > 0)
                {
                    tcReplacementOlderThan24Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Replacement\", \"24Hours\")'>" + olderThan24Hours.ToString() + "</a>";
                }
                else
                {
                    tcReplacementOlderThan24Hours.Text = olderThan24Hours.ToString();
                }

                if (ReplacementList.Count > 0)
                {
                    tcAllReplacementsPending.Text = "<a href='javascript:OpenSalesLineWindow(\"Replacement\", \"AllPending\")'>" + ReplacementList.Count.ToString() + "</a>";
                }
                else
                {
                    tcAllReplacementsPending.Text = ReplacementList.Count.ToString();
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