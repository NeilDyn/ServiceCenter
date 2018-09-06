using ExcelDesign.Class_Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ExcelDesign.Forms.UserControls.StatisticsControls
{
    public partial class Pending_Unknown : System.Web.UI.UserControl
    {
        public List<StatisticsSalesLine> UnknownList { get; set; }

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
                tblPendingUnknown.Rows.Add(breakRow);

                int invNotAvail = 0;
                int olderThan72Hours = 0;
                int olderThan48Hours = 0;

                List<StatisticsSalesLine> olderThan72HoursList = new List<StatisticsSalesLine>();
                List<StatisticsSalesLine> olderThan48HoursList = new List<StatisticsSalesLine>();
                List<StatisticsSalesLine> invNotAvailList = new List<StatisticsSalesLine>();

                foreach (StatisticsSalesLine line in UnknownList)
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
                    tcUnknownOlderThan72Hours.Text = olderThan72Hours.ToString();
                }
                else
                {
                    tcUnknownOlderThan72Hours.Text = olderThan72Hours.ToString();
                }

                if (invNotAvail > 0)
                {
                    //tcUnknownNoInvAvail.Text = "<a href='javascript:expandInvNotAvailUnknown()'>" + invNotAvail.ToString() + "</a>";
                    tcUnknownNoInvAvail.Text = invNotAvail.ToString();
                }
                else
                {
                    tcUnknownNoInvAvail.Text = invNotAvail.ToString();
                }

                if (olderThan48Hours > 0)
                {
                    tcUnknownOlderThan48Hours.Text = olderThan48Hours.ToString();
                }
                else
                {
                    tcUnknownOlderThan48Hours.Text = olderThan48Hours.ToString();
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