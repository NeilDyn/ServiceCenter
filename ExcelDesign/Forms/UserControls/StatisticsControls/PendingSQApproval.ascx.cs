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

                List<StatisticsSalesLine> olderThan72HoursList = new List<StatisticsSalesLine>();
                List<StatisticsSalesLine> olderThan48HoursList = new List<StatisticsSalesLine>();
                List<StatisticsSalesLine> invNotAvailList = new List<StatisticsSalesLine>();

                foreach (StatisticsSalesLine line in SQList)
                {
                    if (line.IsOlderThan72Hours)
                    {
                        olderThan72Hours++;
                        olderThan72HoursList.Add(line);
                    }
                    else if (line.IsOlderThan48Hours)
                    {
                        olderThan48Hours++;
                        olderThan48HoursList.Add(line);
                    }

                    if (line.IsNotInvtAvailable)
                    {
                        invNotAvail++;
                        invNotAvailList.Add(line);
                    }
                }

                if (olderThan72Hours > 0)
                {
                    //tcUnknownOlderThan72Hours.Text = "<a href='javascript:expandUnknownOlderThan72Hours()'>" + olderThan72Hours.ToString() + "</a>";
                    tcSQOlderThan72Hours.Text = olderThan72Hours.ToString();
                }
                else
                {
                    tcSQOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    //tcUnknownNoInvAvail.Text = "<a href='javascript:expandInvNotAvailUnknown()'>" + invNotAvail.ToString() + "</a>";
                    tcSQNoInvAvail.Text = invNotAvail.ToString();
                }
                else
                {
                    tcSQNoInvAvail.Text = invNotAvail.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcSQOlderThan48Hours.Text = olderThan48Hours.ToString();
                }
                else
                {
                    tcSQOlderThan48Hours.Text = olderThan48Hours.ToString();
                }

                //PopulateNoInventoryLines(invNotAvailList);
                //PopulateOlderThan72HoursLines(olderThan72HoursList);
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