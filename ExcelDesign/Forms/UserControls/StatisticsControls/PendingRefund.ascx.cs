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

    public partial class PendingRefund : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> RefundList { get; set; }

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
                tblPendingRefunds.Rows.Add(breakRow);

                int olderThan72Hours = 0;
                int olderThan48Hours = 0;
                int olderThan24Hours = 0;

                foreach (StatisticsSalesLine line in RefundList)
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
                }

                if (olderThan72Hours > 0)
                {
                    tcRefundOlder72Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Refund\", \"72Hours\")'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcRefundOlder72Hours.Text = olderThan72Hours.ToString();
                }

                if(olderThan48Hours > 0)
                {
                    tcRefundOlder48Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Refund\", \"48Hours\")'>" + olderThan48Hours.ToString() + "</a>";
                }
                else
                {
                    tcRefundOlder48Hours.Text = olderThan48Hours.ToString();
                }

                if (olderThan24Hours > 0)
                {
                    tcRefundOlder24Hours.Text = "<a href='javascript:OpenSalesLineWindow(\"Refund\", \"24Hours\")'>" + olderThan24Hours.ToString() + "</a>";
                }
                else
                {
                    tcRefundOlder24Hours.Text = olderThan24Hours.ToString();
                }

                if (RefundList.Count > 0)
                {
                   tcAllRefundsPending.Text = "<a href='javascript:OpenSalesLineWindow(\"Refund\", \"AllPending\")'>" + RefundList.Count.ToString() + "</a>";
                }
                else
                {
                    tcAllRefundsPending.Text = RefundList.Count.ToString();
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