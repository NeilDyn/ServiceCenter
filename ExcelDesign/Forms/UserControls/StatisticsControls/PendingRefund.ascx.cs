using ExcelDesign.Class_Objects;
using ExcelDesign.Forms.UserControls.StatisticsControls.SalesLInes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class PendingRefund : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> RefundList { get; set; }

        protected TableCell linesCell;

        protected Control linesControl;

        protected const string linesPath = "SalesLines/RefundOlder72HoursLines.ascx";

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

                List<StatisticsSalesLine> olderThan72HoursList = new List<StatisticsSalesLine>();

                foreach (StatisticsSalesLine line in RefundList)
                {
                    if (line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                        olderThan72HoursList.Add(line);
                    }
                }

                if (olderThan72Hours > 0)
                {
                    tcRefundOlder72Hours.Text = "<a href='javascript:expandRefundOlderThan72Hours()'>" + olderThan72Hours.ToString() + "</a>";
                }
                else
                {
                    tcRefundOlder72Hours.Text = olderThan72Hours.ToString();
                }

                PopulateOlder72Hours(olderThan72HoursList);
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

        protected void PopulateOlder72Hours(List<StatisticsSalesLine> olderThan72HoursList)
        {
            linesCell = new TableCell();
            linesControl = LoadControl(linesPath);
            ((RefundLines)linesControl).OlderThan72HoursList = olderThan72HoursList;
            ((RefundLines)linesControl).PopulateData();

            linesCell.Controls.Add(linesControl);
            linesCell.ColumnSpan = 4;
            linesCell.Height = new Unit("100%");
            linesCell.Width = new Unit("100%");
            expandRefundOlder72Hours.Cells.Add(linesCell);
        }
    }
}